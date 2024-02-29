using Api.Data.Context;
using Api.Domain.DTOs.Gasto;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class GastoRepository : IGastoRepository
{
    private readonly MyContext _context;
    private readonly DbSet<GastoEntity> _gastoTbl;
    private readonly DbSet<UsuarioEntity> _usuTbl;
    private readonly DbSet<IntervCalcGastoEntity> _intervCalcTbl;

    public GastoRepository(MyContext context)
    {
        _context = context;
        _gastoTbl = _context.Set<GastoEntity>();
        _usuTbl = _context.Set<UsuarioEntity>();
        _intervCalcTbl = _context.Set<IntervCalcGastoEntity>();
    }

    public async Task<GastoEntity?> Create(GastoEntity despesa)
    {
        var exists = await _gastoTbl
                            .Where(g => g.Usuario == despesa.Usuario)
                            .AnyAsync(g => g.Tipo == despesa.Tipo);

        if (exists)
        {
            return null;
        }

        await Task.WhenAll(
            _gastoTbl.AddAsync(despesa).AsTask(),
            _context.SaveChangesAsync()
        );

        return despesa;
    }

    public async Task<bool> Delete(string usu, string tipo)
    {
        try
        {
            var gastoToDelete = await Get(usu, tipo);

            if (gastoToDelete == null)
            {
                return false;
            }

            _gastoTbl.Remove(gastoToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO EXCLUIR GASTO => ", ex);
        }
    }

    public async Task<List<GastoEntity>> Get(string usuario)
    {
        try
        {
            return await _gastoTbl
                            .Where(g =>
                                g.Usuario == usuario)
                            .AsNoTracking()
                            .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER LISTA DE GASTOS => ", ex);
        }
    }

    public async Task<GastoEntity?> Get(string usu, string tipo)
    {
        try
        {
            var keys = new string[] { usu, tipo };

            return await _gastoTbl.FindAsync(keys);
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER GASTO DESEJADO => ", ex);
        }
    }

    public async Task<IntervCalcGastoEntity> GetDatas()
    {
        try
        {
            var datasTbl = await _intervCalcTbl.FindAsync(1);

            var dataHoje = DateTime.Today;

            if(dataHoje < datasTbl!.DataFinal)
            {
                var newDatas = IntervCalcGastoEntity
                                .MontarDatas(dataHoje);

                return await UpdateDatas(newDatas, datasTbl);
            }

            return datasTbl;
        }
        catch(Exception ex)
        {
            throw new Exception("ERRO AO OBTER DATAS DE INTERVALO DE CÁLCULO => ", ex);
        }
    }

    public async Task<IntervCalcGastoEntity> UpdateDatas(
        IntervCalcGastoEntity newDatas, 
        IntervCalcGastoEntity datasToUpdate
        )
    {
        try
        {
            datasToUpdate!.DataInicio = newDatas.DataInicio;
            datasToUpdate.DataFinal = newDatas.DataFinal;

            await _context.SaveChangesAsync();
            return newDatas;
        }
        catch(Exception ex)
        {
            throw new Exception("ERRO AO ATUALIZAR DATAS => ", ex);
        }
    }

    public async Task<double?> GetSalario(string usuario)
    {
        try
        {
            return await _usuTbl
                            .Where(u => u.Usuario == usuario)
                            .AsNoTracking()
                            .Select(u => u.Salario)
                            .SingleOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER SALÁRIO => ", ex);
        }
    }

    public async Task<UsuarioEntity?> GetUsuLogado()
    {
        try
        {
            return await _usuTbl
                            .AsNoTracking()
                            .SingleOrDefaultAsync(u =>
                                u.Logado == true);
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER USUÁRIO LOGADO => ", ex);
        }
    }

    public async Task<bool> SalarioCadastrado()
    {
        try
        {
            var usuLogado = await GetUsuLogado();

            if (usuLogado!.Salario == null)
            {
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO VERIFICAR SALÁRIO => ", ex);
        }
    }

    public async Task<GastoEntity?> Update(GastoEntity gasto)
    {
        var usu = gasto.Usuario;
        var tipo = gasto.Tipo;

        var gastoToUpdate = await Get(usu, tipo);

        if (gastoToUpdate == null)
        {
            return null;
        }

        _gastoTbl
        .Entry(gastoToUpdate)
        .CurrentValues
        .SetValues(gasto);

        await _context.SaveChangesAsync();
        return gasto;
    }


    public async Task<UsuarioEntity?> UpdateSalario(double salario)
    {
        try
        {
            var usuToUpdate = await _usuTbl
                                    .SingleOrDefaultAsync(u => u.Logado == true);

            if (usuToUpdate == null)
            {
                return null;
            }

            usuToUpdate.Salario = salario;
            await _context.SaveChangesAsync();

            return usuToUpdate;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO ATUALIZAR SALÁRIO DE USUÁRIO => ", ex);
        }
    }

}
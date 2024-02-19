using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class GastoRepository : IGastoRepository
{
    private readonly MyContext _context;
    private readonly DbSet<GastoEntity> _gastoTbl;
    private readonly DbSet<UsuarioEntity> _usuTbl;

    public GastoRepository(MyContext context)
    {
        _context = context;
        _gastoTbl = _context.Set<GastoEntity>();
        _usuTbl = _context.Set<UsuarioEntity>();
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
            var chaves = new string[] { usu, tipo };

            var gastoToDelete = await _gastoTbl
                                        .FindAsync(chaves);

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

    public async Task<List<GastoEntity>> Get()
    {
        try
        {
            var usuLogado = await GetUsuLogado();

            return await _gastoTbl
                            .Where(g => g.Usuario == usuLogado)
                            .AsNoTracking()
                            .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER LISTA DE GASTOS => ", ex);
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

    public async Task<string?> GetUsuLogado()
    {
        try
        {
            return await _usuTbl
                            .Where(u => u.Logado == true)
                            .AsNoTracking()
                            .Select(u => u.Usuario)
                            .SingleOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO OBTER USUÁRIO LOGADO => ", ex);
        }
    }

    public async Task<GastoEntity?> Update(GastoEntity gasto)
    {
        var gastoToUpdate = await _gastoTbl
                                    .SingleOrDefaultAsync(g =>
                                        g.Usuario == gasto.Usuario &&
                                        g.Tipo == gasto.Tipo);

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

    public async Task<UsuarioEntity?> UpdateSalario(double newSalario)
    {
        try
        {
            var usuToUpdate = await _usuTbl
                                    .SingleOrDefaultAsync(u => u.Logado == true);

            if (usuToUpdate == null)
            {
                return null;
            }

            usuToUpdate.Salario = newSalario;
            await _context.SaveChangesAsync();

            return usuToUpdate;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO ATUALIZAR SALÁRIO DE USUÁRIO => ", ex);
        }
    }
}
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

    public async Task<List<GastoEntity>> Get()
    {
        try
        {
            var usuLogado = await _usuTbl
                                    .Where(u => u.Logado == true)
                                    .AsNoTracking()
                                    .Select(u => u.Usuario)
                                    .SingleOrDefaultAsync();

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
}
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository;

public class LoginRepository : ILoginRepository
{
    private readonly MyContext _context;
    private readonly DbSet<UsuarioEntity> _usuTbl;

    public LoginRepository(MyContext context)
    {
        _context = context;
        _usuTbl = _context.Set<UsuarioEntity>();
    }

    public async Task<bool> SignOff()
    {
        try
        {
            var usuLogado = await _usuTbl
                                    .SingleOrDefaultAsync(u => 
                                        u.Logado == true);

            if(usuLogado == null)
            {
                return false;
            }

            usuLogado!.Logado = false;
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO DESLOGAR USUÁRIO => ", ex);
        }
    }

    public async Task<bool> SingIn(UsuarioEntity login)
    {
        try
        {
            await SignOff();

            var usuario = await _usuTbl
                                .SingleOrDefaultAsync(u => 
                                    u.Usuario == login.Usuario &&
                                    u.Senha == login.Senha);

            if (usuario == null)
            {
                return false;
            }

            usuario!.Logado = true;
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO REALIZAR LOGIN => ", ex);
        }
    }

    public async Task<UsuarioEntity?> SingUp(UsuarioEntity newUsu)
    {
        try
        {
            var exists = await _usuTbl
                                .AnyAsync(u => u.Usuario == newUsu.Usuario);

            if (exists)
            {
                return null;
            }

            await Task.WhenAll(
                _usuTbl.AddAsync(newUsu).AsTask(),
                _context.SaveChangesAsync()
            );

            return newUsu;
        }
        catch (Exception ex)
        {
            throw new Exception("ERRO AO REALIZAR CADSTRO => ", ex);
        }
    }
}
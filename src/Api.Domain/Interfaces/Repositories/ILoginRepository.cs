using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface ILoginRepository
{
    Task<bool> Deslogar();
    Task<bool> SingIn(UsuarioEntity usuario);
    Task<UsuarioEntity?> SingUp(UsuarioEntity usuario); 
}
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface ILoginRepository
{
    Task<bool> SignOff();
    Task<bool> SingIn(UsuarioEntity usuario);
    Task<UsuarioEntity?> SingUp(UsuarioEntity usuario);
    // Task<UsuarioEntity?> UpdateSalario(double newSalario);
}
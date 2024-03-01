using Api.Domain.DTOs;
using Api.Domain.DTOs.Login;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface ILoginService
{
    Task<RespostaEntity> SignUp(SignUpDto newUsu);
    Task<RespostaEntity> SignIn(UsuarioEntity usuario);
    Task<RespostaEntity> SignOff();
}
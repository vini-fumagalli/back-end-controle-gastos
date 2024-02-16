using Api.Domain.DTOs;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface ILoginService
{
    Task<RespostaEntity> SignUp(SignUpDto newUsu);
}
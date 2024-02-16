using Api.Domain.DTOs;
using Api.Domain.DTOs.Login;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _repository;
    private readonly IMapper _mapper;

    public LoginService(ILoginRepository repository,
                        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RespostaEntity> SignOff()
    {
        var resposta = await _repository.SignOff();
        return new RespostaEntity
        {
            Sucesso = resposta,
            Resposta = resposta
        };
    }

    public async Task<RespostaEntity> SignIn(UsuarioEntity usuario)
    {
        var resposta = await _repository.SingIn(usuario);
        return new RespostaEntity
        {
            Sucesso = resposta,
            Resposta = resposta
        };
    }

    public async Task<RespostaEntity> SignUp(SignUpDto signUp)
    {
        var newUsu = _mapper.Map<UsuarioEntity>(signUp);

        var resposta = await _repository.SingUp(newUsu);
        return new RespostaEntity
        {
            Sucesso = resposta != null,
            Resposta = resposta
        };
    }
}
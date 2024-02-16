using Api.Domain.DTOs;
using Api.Domain.DTOs.Login;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.AutoMapper;

public class LoginProfile : Profile
{
    public LoginProfile()
    {
        CreateMap<SignUpDto, UsuarioEntity>()
        .ForMember(dest => dest.Logado, opt => opt.MapFrom(src => false));
    }
}
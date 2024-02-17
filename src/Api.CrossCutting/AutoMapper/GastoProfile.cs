using Api.Domain.DTOs.Gasto;
using Api.Domain.Entities;
using AutoMapper;

namespace Api.CrossCutting.AutoMapper;

public class GastoProfile : Profile
{
    public GastoProfile()
    {
        CreateMap<CreateGastoDto, GastoEntity>()
        .ForMember(dest => dest.Pago, opt => opt.MapFrom(src => false));

        CreateMap<UpdateGastoDto, GastoEntity>();
        
    }
}
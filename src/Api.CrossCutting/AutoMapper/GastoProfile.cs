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

        CreateMap<GastoEntity, GastoDtoResult>()
        .AfterMap((src, dest) =>
        {
            dest.DataMax = FormatDate(src.DataMax);
        });

    }

    public string? FormatDate(DateTime? data)
    {
        return data?.ToString("dd/MM/yyyy");
    }
}
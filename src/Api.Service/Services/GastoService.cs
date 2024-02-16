using System.Globalization;
using Api.Domain.DTOs.Gasto;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repositories;
using Api.Domain.Interfaces.Services;
using AutoMapper;

namespace Api.Service.Services;

public class GastoService : IGastoService
{
    private readonly IGastoRepository _repository;
    private readonly IMapper _mapper;

    public GastoService(IGastoRepository repository,
                        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<RespostaEntity> Create(CreateGastoDto dto)
    {
        var despesa = _mapper.Map<GastoEntity>(dto);

        var resposta = await _repository.Create(despesa);
        return new RespostaEntity
        {
            Sucesso = resposta != null,
            Resposta = resposta
        };
    }

    public async Task<RespostaEntity?> Get()
    {
        var gastosList = await _repository.Get();
        if(!gastosList.Any())
        {
            return null;
        }

        var salario = await _repository
                            .GetSalario(gastosList[0].Usuario);

        var dto = GetGastosDto.MontarDto(gastosList, salario);
        
        return new RespostaEntity
        {
            Sucesso = dto.Gastos!.Any(),
            Resposta = dto
        };
    }

    public async Task<RespostaEntity> Update(GastoEntity gasto)
    {
        var resposta = await _repository.Update(gasto);
        
        return new RespostaEntity
        {
            Sucesso = resposta != null,
            Resposta = resposta
        };
    }
}
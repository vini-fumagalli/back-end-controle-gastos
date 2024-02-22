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

        var usuLogado = await _repository.GetUsuLogado();
        despesa.Usuario = usuLogado!;

        var resposta = await _repository.Create(despesa);
        return new RespostaEntity
        {
            Sucesso = resposta != null,
            Resposta = resposta
        };
    }

    public async Task<RespostaEntity> Delete(string tipo)
    {
        var usu = await _repository.GetUsuLogado();

        var resposta = await _repository.Delete(usu!, tipo);

        return new RespostaEntity
        {
            Sucesso = resposta,
            Resposta = resposta
        };
    }

    public async Task<RespostaEntity?> Get()
    {
        var gastosList = await _repository.Get();
        if (!gastosList.Any())
        {
            return null;
        }

        var salario = await _repository
                            .GetSalario(gastosList[0].Usuario);

        var dto = GetGastosDto.MontarDto(gastosList, salario);

        var gastoListDto = _mapper.Map<List<GastoDtoResult>>(gastosList);
        dto.Gastos = gastoListDto;

        return new RespostaEntity
        {
            Sucesso = dto.Gastos!.Any(),
            Resposta = dto
        };
    }

    public async Task<RespostaEntity> Get(string usu, string tipo)
    {
        var resposta = await _repository.Get(usu, tipo);
        
        return new RespostaEntity
        {
            Sucesso = true,
            Resposta = resposta
        };
    }

    public async Task<RespostaEntity> Update(UpdateGastoDto dto)
    {
        var usuLogado = await _repository.GetUsuLogado();

        var gasto = _mapper.Map<GastoEntity>(dto);
        gasto.Usuario = usuLogado!;

        var resposta = await _repository.Update(gasto);

        return new RespostaEntity
        {
            Sucesso = resposta != null,
            Resposta = resposta
        };
    }

    public async Task<RespostaEntity> UpdateSalario(double newSalario)
    {
        var resposta = await _repository.UpdateSalario(newSalario);

        return new RespostaEntity
        {
            Sucesso = resposta != null,
            Resposta = resposta
        };
    }
}
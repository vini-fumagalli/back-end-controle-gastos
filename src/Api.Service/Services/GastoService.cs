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

    public async Task<RespostaEntity> Get()
    {
        var resposta = await _repository.Get();

        // var mesAtual = DateTime.Now.Month;
        // var anoAtual = DateTime.Now.Year;
        // var proxMes = mesAtual + 1;
        // var proxAno = anoAtual;

        // if (proxMes > 12)
        // {
        //     proxAno++;
        //     proxMes = 1;
        // }

        // var dataIni = new DateTime(anoAtual, mesAtual, 6);
        // var dataFim = new DateTime(proxAno, proxMes, 6);

        // resposta = resposta
        //             .Where(g => g.DataMax >= dataIni &&
        //                         g.DataMax <= dataFim)
        //             .ToList();

        return new RespostaEntity
        {
            Sucesso = resposta.Any(),
            Resposta = resposta
        };
    }
}
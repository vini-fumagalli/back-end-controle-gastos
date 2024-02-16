using Api.Domain.DTOs.Gasto;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IGastoService
{
    Task<RespostaEntity> Get();
    Task<RespostaEntity> Create(CreateGastoDto dto);
}
using Api.Domain.DTOs.Gasto;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Services;

public interface IGastoService
{
    Task<RespostaEntity> Get();
    Task<RespostaEntity> Create(CreateGastoDto dto);
    Task<RespostaEntity> Update(UpdateGastoDto dto);
    Task<RespostaEntity> UpdateSalario(UpdateSalarioDto dto);
    Task<RespostaEntity> Delete(string tipo);
    Task<RespostaEntity> Get(string usu, string tipo);
    Task<RespostaEntity> SalarioCadastrado();
}
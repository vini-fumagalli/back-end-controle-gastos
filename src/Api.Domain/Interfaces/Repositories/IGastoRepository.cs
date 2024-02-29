using Api.Domain.DTOs.Gasto;
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface IGastoRepository
{
    Task<List<GastoEntity>> Get(string usuario);
    Task<GastoEntity?> Create(GastoEntity despesa);
    Task<double?> GetSalario(string usuario);
    Task<GastoEntity?> Update(GastoEntity gasto);
    Task<UsuarioEntity?> UpdateSalario(double salario);
    Task<UsuarioEntity?> GetUsuLogado();
    Task<bool> Delete(string usu, string tipo);
    Task<GastoEntity?> Get(string usu, string tipo);
    Task<bool> SalarioCadastrado();
    Task<IntervCalcGastoEntity> GetDatas();
    Task<IntervCalcGastoEntity> UpdateDatas(IntervCalcGastoEntity newDatas, IntervCalcGastoEntity datasToUpdate);
}
using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repositories;

public interface IGastoRepository
{
    Task<List<GastoEntity>> Get();
    Task<GastoEntity?> Create(GastoEntity despesa);
    Task<double?> GetSalario(string usuario);
    Task<GastoEntity?> Update(GastoEntity gasto);
}
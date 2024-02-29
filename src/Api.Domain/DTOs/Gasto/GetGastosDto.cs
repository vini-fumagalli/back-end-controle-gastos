using Api.Domain.Entities;

namespace Api.Domain.DTOs.Gasto;

public class GetGastosDto
{
    public List<GastoDtoResult>? Gastos { get; set; }
    public double? Total { get; set; }
    public double? GastoMax { get; set; }

    public static GetGastosDto MontarDto(
        List<GastoEntity> list,
        IntervCalcGastoEntity datasCalc,
        double? salario
        )
    {
        if (list.Count == 0)
        {
            return new GetGastosDto
            {
                Total = 0,
                GastoMax = salario
            };
        }

        var dataIni = datasCalc.DataInicio;
        var dataFim = datasCalc.DataFinal;

        var filtroMes = list
                        .Where(g =>
                            g.DataMax >= dataIni &&
                            g.DataMax <= dataFim &&
                            g.Pago == false)
                        .ToList();

        var total = filtroMes.Sum(f => f.Valor);

        var gastoMax = salario - total;
        gastoMax = gastoMax > 0 ? gastoMax : 0;

        return new GetGastosDto
        {
            Total = total,
            GastoMax = gastoMax
        };
    }

    



}



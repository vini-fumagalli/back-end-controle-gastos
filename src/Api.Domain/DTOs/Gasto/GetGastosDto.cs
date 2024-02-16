using Api.Domain.Entities;

namespace Api.Domain.DTOs.Gasto;

public class GetGastosDto
{
    public List<GastoEntity>? Gastos { get; set; }
    public double? Total { get; set; }
    public double? GastoMax { get; set; }

    public static GetGastosDto MontarDto(List<GastoEntity> list, 
                                         double? salario)
    {
        var mesAtual = DateTime.Now.Month;
        var anoAtual = DateTime.Now.Year;
        var proxMes = mesAtual + 1;
        var proxAno = anoAtual;

        if (proxMes > 12)
        {
            proxAno++;
            proxMes = 1;
        }

        var dataIni = new DateTime(anoAtual, mesAtual, 6);
        var dataFim = new DateTime(proxAno, proxMes, 6);

        var filtroMes = list
                        .Where(g => g.DataMax >= dataIni &&
                                    g.DataMax <= dataFim)
                        .ToList();

        var total = filtroMes.Sum(f => f.Valor);

        var gastoMax = salario - total;
        gastoMax = gastoMax > 0 ? gastoMax : 0; 

        return new GetGastosDto
        {
            Gastos = list,
            Total = total,
            GastoMax = gastoMax
        };
    }
}



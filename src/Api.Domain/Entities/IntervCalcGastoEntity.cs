using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities;

public class IntervCalcGastoEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }

    public static IntervCalcGastoEntity MontarDatas(DateTime dataNova)
    {
        var mes = dataNova.Month;
        var ano = dataNova.Year;
        var mesSeguinte = mes + 1;
        var anoSeguinte = ano;

        if(mesSeguinte > 12)
        {
            anoSeguinte++;
            mesSeguinte = 1;
        }

        var dataIniBase = new DateTime(ano, mes, 1);
        var dataFimBase = new DateTime(anoSeguinte, mesSeguinte, 1);

        return new IntervCalcGastoEntity
        {
            DataInicio = GetQuintoDiaUtil(dataIniBase),
            DataFinal = GetQuintoDiaUtil(dataFimBase)
        };
    } 

    public static DateTime GetQuintoDiaUtil(DateTime data)
    {
        int diasUteis = 0;
        DateTime dataLoop;

        for (dataLoop = data; diasUteis != 5 ; dataLoop = dataLoop.AddDays(1))
        {
            diasUteis += VerificaFimDeSemana(dataLoop);
        }

        return new DateTime(data.Year, data.Month, dataLoop.Day - 1);
    }

     public static int VerificaFimDeSemana(DateTime data)
    {
        if (data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday)
        {
            return 1;
        }

        return 0;
    }
}
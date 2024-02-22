namespace Api.Domain.DTOs.Gasto;

public class CreateGastoDto
{
    public string Tipo { get; set; } = "";
    public double? Valor { get; set; }
    public DateTime? DataMax { get; set; }
}
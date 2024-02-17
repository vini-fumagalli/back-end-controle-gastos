namespace Api.Domain.DTOs.Gasto;

public class UpdateGastoDto
{
    public string Tipo { get; set; } = "";
    public double? Valor { get; set; }
    public bool Pago { get; set; }
    public DateTime? DataMax { get; set; }
}
namespace Api.Domain.DTOs.Gasto;

public class GastoDtoResult
{
    public string Tipo { get; set; } = "";
    public double? Valor { get; set; }
    public bool Pago { get; set; }
    public string? DataMax { get; set; }
}
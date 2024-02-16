using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Entities;

public class GastoEntity
{
    [ForeignKey("Usuario")]
    public string Usuario { get; set; } = "";
    [Key]
    public string Tipo { get; set; } = "";
    public decimal? Valor { get; set; }
    public bool Pago { get; set; }
    public DateTime? DataMax { get; set; }
}
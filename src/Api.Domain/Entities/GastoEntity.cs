using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Domain.Entities;

public class GastoEntity
{
    [ForeignKey("Usuario")]
    public virtual UsuarioEntity UsuarioNavigation { get; set; } = new UsuarioEntity();

    [Key, Column(Order = 0)]
    public string Usuario { get; set; } = "";
    
    [Key, Column(Order = 1)]
    public string Tipo { get; set; } = "";
    public double? Valor { get; set; }
    public bool Pago { get; set; }
    public DateTime? DataMax { get; set; }
}
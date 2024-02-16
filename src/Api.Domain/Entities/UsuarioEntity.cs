using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities;

public class UsuarioEntity
{
    [Key]
    public string Usuario { get; set; } = "";
    public string Senha { get; set; } = "";
    public bool Logado { get; set; }
    public double? Salario { get; set; }
}
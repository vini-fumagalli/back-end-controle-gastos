namespace Api.Domain.DTOs;

public class SignUpDto
{
    public string Usuario { get; set; } = "";
    public string Senha { get; set; } = "";
    public string ConfirmaSenha { get; set; } = "";
}
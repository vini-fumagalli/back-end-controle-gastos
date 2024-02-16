namespace Api.Domain.DTOs.Login;

public class SignUpDto
{
    public string Usuario { get; set; } = "";
    public string Senha { get; set; } = "";
    public string ConfirmaSenha { get; set; } = "";
}
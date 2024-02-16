using Api.Domain.DTOs;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Application.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _service;

    public LoginController(ILoginService service)
    {
        _service = service;
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult<RespostaEntity>> SignUp(SignUpDto signUp)
    {
        try
        {
            if(signUp.Senha != signUp.ConfirmaSenha)
            {
                return BadRequest();
            }

            var resp = await _service.SignUp(signUp);
            return resp.Sucesso == true ? Ok(resp) : Conflict();
        }
        catch(Exception ex)
        {
            Log.Error(ex.Message);
            throw new Exception("ERRO AO REALIZAR CADASTRO => ", ex);
        }
    }

}

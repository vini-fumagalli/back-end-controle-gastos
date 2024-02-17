using Api.Domain.DTOs.Gasto;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Api.Application.Controllers;

[ApiController]
[Route("api/gasto")]
public class GastoController : ControllerBase
{
    private readonly IGastoService _service;

    public GastoController(IGastoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<RespostaEntity>> Get()
    {
        try
        {
            var resposta =  await _service.Get();
            return Ok(resposta);
        }
        catch(Exception ex)
        {
            Log.Error(ex.Message);
            throw new Exception("ERRO AO OBTER LISTA DE GASTOS => ", ex);
        }
    }

    [HttpPost]
    public async Task<ActionResult<RespostaEntity>> Create(CreateGastoDto dto)
    {
        try
        {
            var resposta =  await _service.Create(dto);
            return Ok(resposta);
        }
        catch(Exception ex)
        {
            Log.Error(ex.Message);
            throw new Exception("ERRO AO CADASTRAR NOVO GASTO => ", ex);
        }
    }

    [HttpPut]
    public async Task<ActionResult<RespostaEntity>> Update(UpdateGastoDto dto)
    {
        try
        {
            var resposta =  await _service.Update(dto);
            return Ok(resposta);
        }
        catch(Exception ex)
        {
            Log.Error(ex.Message);
            throw new Exception("ERRO AO ATUALIZAR SALÁRIO DE USUÁRIO => ", ex);
        }
    }

    [HttpPut("salario/{salario:double}")]
    public async Task<ActionResult<RespostaEntity>> UpdateSalario(double salario)
    {
        try
        {
            var resposta =  await _service.UpdateSalario(salario);
            return Ok(resposta);
        }
        catch(Exception ex)
        {
            Log.Error(ex.Message);
            throw new Exception("ERRO AO ATUALIZAR SALÁRIO DE USUÁRIO => ", ex);
        }
    }

    [HttpDelete("{tipo:string}")]
    public async Task<ActionResult<RespostaEntity>> Delete(string tipo)
    {
        try
        {
            var resposta =  await _service.Delete(tipo);
            return Ok(resposta);
        }
        catch(Exception ex)
        {
            Log.Error(ex.Message);
            throw new Exception("ERRO AO ATUALIZAR SALÁRIO DE USUÁRIO => ", ex);
        }
    }
}
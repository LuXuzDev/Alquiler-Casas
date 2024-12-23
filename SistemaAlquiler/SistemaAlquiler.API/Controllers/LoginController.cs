using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class LoginController: ControllerBase
{
    private readonly ILoginServicio loginServicio;



    public LoginController(ILoginServicio loginServicio)
    {
        this.loginServicio = loginServicio;
    }

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginDTO loginDTO)
    {
        var token = await loginServicio.login(loginDTO.nombreUsuario,loginDTO.correo,loginDTO.clave);
        return StatusCode(StatusCodes.Status200OK, token);
    }
}

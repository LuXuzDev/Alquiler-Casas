using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAdmin")]
public class AdministradorController:ControllerBase
{
    private readonly IAdministradorServicio administradorServicio;

    public AdministradorController(IAdministradorServicio administradorServicio)
    {
        this.administradorServicio = administradorServicio;
    }

    [HttpGet("gananciasMensuales")]
    public async Task<IActionResult> ganaciasMensuales()
    {
        return StatusCode(StatusCodes.Status200OK,await administradorServicio.ganaciasMensuales());
    }

    [HttpGet("gananciasTotales")]
    public async Task<IActionResult> gananciaTotal()
    {
        return StatusCode(StatusCodes.Status200OK,await administradorServicio.ganaciasTotales());
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System.Web.Http.Results;
using SistemaAlquiler.LogicaNegocio.Implementaciones;

namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CalendarioController:ControllerBase
{
    private readonly ICalendarioServicio calendarioServicio;

    public CalendarioController(ICalendarioServicio calendarioServicio)
    {
        this.calendarioServicio = calendarioServicio;
    }


    [HttpGet("listaFechas")]
    public async Task<IActionResult> listaFechas(int idCasa)
    {
        var fechas = await calendarioServicio.fechasOcupadas(idCasa);
        return StatusCode(StatusCodes.Status200OK, fechas);
    }
}

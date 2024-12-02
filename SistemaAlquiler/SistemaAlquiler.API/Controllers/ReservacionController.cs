using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.API.DTOs;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;


[ApiController]
[Route("reservaciones/")]
public class ReservacionController : ControllerBase
{
    private readonly IReservacionServicio reservacionServicio;
    private readonly IMapper autoMapper;


    public ReservacionController(IReservacionServicio reservacionServicio, IMapper autoMapper)
    {
        this.reservacionServicio = reservacionServicio;
        this.autoMapper = autoMapper;
    }

    [HttpGet("precio")]
    public async Task<IActionResult> ciudadID(int idUsuario, int idCasa, int cantPersonas, DateTime fechaEntrada, DateTime fechaSalida)
    {
        var precio = await reservacionServicio.creara( idUsuario,  idCasa,  cantPersonas,  fechaEntrada,  fechaSalida);
        
        return StatusCode(StatusCodes.Status200OK,precio);
    }
}

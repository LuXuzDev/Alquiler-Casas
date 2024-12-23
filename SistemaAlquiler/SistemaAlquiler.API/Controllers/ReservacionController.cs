using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ReservacionController : ControllerBase
{
    private readonly IReservacionServicio reservacionServicio;
    private readonly IMapper autoMapper;


    public ReservacionController(IReservacionServicio reservacionServicio, IMapper autoMapper)
    {
        this.reservacionServicio = reservacionServicio;
        this.autoMapper = autoMapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> obtenerReservacionID(int id)
    {
        var reservacion = await reservacionServicio.obtenerPorId(id);

        ReservacionDTO vmReservacion = autoMapper.Map<ReservacionDTO>(reservacion);

        return StatusCode(StatusCodes.Status200OK, vmReservacion);
    }


    [HttpGet("lista")]
    public async Task<IActionResult> listaReservaciones()
    {
        var reservacion = await reservacionServicio.lista();

        List<ReservacionDTO> vmReservacion = autoMapper.Map<List<ReservacionDTO>>(reservacion);

        return StatusCode(StatusCodes.Status200OK, vmReservacion);
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> eliminar(int id)
    {
        var reservacion = await reservacionServicio.eliminar(id);

        ReservacionDTO vmReservacion = autoMapper.Map<ReservacionDTO>(reservacion);

        return StatusCode(StatusCodes.Status200OK, vmReservacion);
    }

    [HttpPost()]
    public async Task<IActionResult> crear(CrearReservacionDTO reservacionDTO)
    {
        var reservacion = await reservacionServicio.crear(reservacionDTO.idUsuario, reservacionDTO.idCasa,
            reservacionDTO.cantPersonas,  reservacionDTO.fechaEntrada,  reservacionDTO.fechaSalida);

        ReservacionDTO vmReservacion = autoMapper.Map<ReservacionDTO>(reservacion);
        
        return StatusCode(StatusCodes.Status200OK, vmReservacion);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAdmin_Gestor")]

public class GestorController:ControllerBase
{
    private readonly IGestorServicio gestorServicio;
    private readonly IMapper autoMapper;


    public GestorController(IGestorServicio gestorServicio, IMapper autoMapper)
    {
        this.gestorServicio = gestorServicio;
        this.autoMapper = autoMapper;
    }

    [HttpGet("{id}/listaCasas")]
    
    public async Task<IActionResult> listaCasasPublicadas(int id)
    {
        var casas = await gestorServicio.listaCasas(id);
        List<CasaDTO> vmCasas = autoMapper.Map<List<CasaDTO>>(casas);
        return StatusCode(StatusCodes.Status200OK, vmCasas);
    }


    [HttpGet("{id}/reservaciones")]
    public async Task<IActionResult> listaReservaciones(int id)
    {
        var reservaciones = await gestorServicio.listaReservacion(id);
        List<ReservacionDTO> vmReservacion = autoMapper.Map<List<ReservacionDTO>>(reservaciones);
        return StatusCode(StatusCodes.Status200OK, vmReservacion);
    }


    [HttpGet("{id}/pendientes")]
    public async Task<IActionResult> listaCasasPendientes(int id)
    {
        var casas = await gestorServicio.casasPendientes(id);
        List<CasaDTO> vmCasas = autoMapper.Map<List<CasaDTO>>(casas);
        return StatusCode(StatusCodes.Status200OK, vmCasas);
    }

    [HttpGet("{id}/ganancias")]
    public async Task<IActionResult> ganacias(int id)
    {
        double ganancias = await gestorServicio.gananciasTotales(id);
        return StatusCode(StatusCodes.Status200OK, ganancias);
    }

    [HttpGet("{id}/ganancias/mensuales")]
    public async Task<IActionResult> ganaciasMensuales(int id)
    {
        double ganancias = await gestorServicio.ganaciasMensuales(id);
        return StatusCode(StatusCodes.Status200OK, ganancias);
    }

    [HttpGet("{id}/valoraciones/casa")]
    public async Task<IActionResult> valoracionesCasa(int id)
    {
        List<Valoracion> valoracion = await gestorServicio.valoracionesCasa(id);
        List<ValoracionDTO> vmValoracion = autoMapper.Map<List<ValoracionDTO>>(valoracion);
        return StatusCode(StatusCodes.Status200OK, vmValoracion);
    }
}

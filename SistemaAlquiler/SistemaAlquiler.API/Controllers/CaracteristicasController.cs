using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("caracteristicas/")]
public class CaracteristicasController : ControllerBase
{
    private readonly ICaracteristicaServicio caracteristicaServicio;

    private readonly IMapper autoMapper;

    public CaracteristicasController(ICaracteristicaServicio caracteristicaServicio, IMapper autoMapper)
    {
        this.caracteristicaServicio = caracteristicaServicio;
        this.autoMapper = autoMapper;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> obtener([FromBody] int idCaracteristica)
    {
        var caracteristica = await caracteristicaServicio.obtenerPorId(idCaracteristica);
        CaracteristicaDTO vmCaracteristica = autoMapper.Map<CaracteristicaDTO>(caracteristica);
        return StatusCode(StatusCodes.Status200OK, vmCaracteristica);
    }

    [HttpPost("crear")]
    public async Task<IActionResult> crear([FromBody]CrearCaracteristicasDTO caracteristicaDTO)
    {
        var caracteristica = await caracteristicaServicio.crear(caracteristicaDTO);
        CrearCaracteristicasDTO vmCaracteristica = autoMapper.Map<CrearCaracteristicasDTO>(caracteristica);
        return StatusCode(StatusCodes.Status200OK, vmCaracteristica);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> eliminar([FromBody] int idCaracteristica)
    {
        var caracteristica = await caracteristicaServicio.eliminar(idCaracteristica);
        CaracteristicaDTO vmCaracteristica = autoMapper.Map<CaracteristicaDTO>(caracteristica);
        return StatusCode(StatusCodes.Status200OK, vmCaracteristica);
    }
}

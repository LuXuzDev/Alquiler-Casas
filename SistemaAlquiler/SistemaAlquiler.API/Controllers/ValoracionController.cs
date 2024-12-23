using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ValoracionController:ControllerBase
{
    private readonly IValoracionServicio valoracionServicio;

    private readonly IMapper autoMapper;


    public ValoracionController(IValoracionServicio valoracionServicio, IMapper autoMapper)
    {
        this.valoracionServicio = valoracionServicio;

        this.autoMapper = autoMapper;
    }



    [HttpGet("/lista")]
    public async Task<IActionResult> lista()
    {
        var valoraciones = await valoracionServicio.lista();

        List<ValoracionDTO> vmLista = autoMapper.Map<List<ValoracionDTO>>(valoraciones);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }



    [HttpPost]
    public async Task<IActionResult> crear( [FromBody] CrearValoracionDTO valoracionDTO)
    {
        Valoracion valoracion = await valoracionServicio.crear(valoracionDTO);
        ValoracionDTO vmValoracion = autoMapper.Map<ValoracionDTO>(valoracion);
        return StatusCode(StatusCodes.Status200OK, vmValoracion);
    }


    [HttpPatch]
    public async Task<IActionResult> editar( [FromBody] ValoracionDTO valoracionDTO)
    {
        Valoracion valoracion = await valoracionServicio.editar(valoracionDTO);
        ValoracionDTO vmValoracion = autoMapper.Map<ValoracionDTO>(valoracion);
        return StatusCode(StatusCodes.Status200OK, vmValoracion);
    }


    [HttpDelete("{idValoracion}")]
    public async Task<IActionResult> eliminar(int idValoracion)
    {
        Valoracion valoracion = await valoracionServicio.eliminar(idValoracion);
        ValoracionDTO vmValoracion = autoMapper.Map<ValoracionDTO>(valoracion);
        return StatusCode(StatusCodes.Status200OK, vmValoracion);
    }


    [HttpGet("{idValoracion}")]
    public async Task<IActionResult> obtenerPorId(int idValoracion)
    {
        Valoracion valoracion = await valoracionServicio.obtenerPorId(idValoracion);
        ValoracionDTO vmValoracion = autoMapper.Map<ValoracionDTO>(valoracion);
        return StatusCode(StatusCodes.Status200OK, vmValoracion);
    }
}

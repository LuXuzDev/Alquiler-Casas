using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;


[ApiController]
[Route("ciudades/")]
public class CiudadController : ControllerBase
{
    private readonly ICiudadServicio ciudadServicio;
    private readonly IMapper autoMapper;


    public CiudadController(ICiudadServicio ciudadServicio,IMapper autoMapper)
    {
        this.ciudadServicio = ciudadServicio;
        this.autoMapper = autoMapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ciudadID(int id)
    {
        var ciudad = await ciudadServicio.obtenerPorId(id);
        CiudadDTO vmCiudad = autoMapper.Map<CiudadDTO>(ciudad);
        return StatusCode(StatusCodes.Status200OK, vmCiudad);
    }


    [HttpGet("listaCiudades")]
    public async Task<IActionResult> listaCiudades()
    {
        var listaCiudades = await ciudadServicio.lista();
        List<CiudadDTO> vmLista = autoMapper.Map<List<CiudadDTO>>(listaCiudades);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }


    [HttpPost("crear")]
    public async Task<IActionResult> crear(string ciudad)
    {
        Ciudad c= await ciudadServicio.crear(ciudad);
        CiudadDTO vmCiudad = autoMapper.Map<CiudadDTO>(c);
        return StatusCode(StatusCodes.Status200OK, vmCiudad);
    }

    [HttpDelete("eliminar")]
    public async Task<IActionResult> eliminar(int idCiudad)
    {
        var eliminado = ciudadServicio.eliminar(idCiudad);
        return StatusCode(StatusCodes.Status200OK);
    }


    [HttpPatch("editar")]
    public async Task<IActionResult> editar(string ciudad, int idCiudad)
    {
        Ciudad actualizado = await ciudadServicio.editar(ciudad,idCiudad);
        CiudadDTO vmCiudad = autoMapper.Map<CiudadDTO>(actualizado);
        return StatusCode(StatusCodes.Status200OK, vmCiudad);

    }
}

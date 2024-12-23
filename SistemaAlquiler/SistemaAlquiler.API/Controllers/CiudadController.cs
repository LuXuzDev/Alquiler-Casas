﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace SistemaAlquiler.API.Controllers;


[ApiController]
[Route("api/[controller]")]
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


    [HttpPost("{ciudad}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> crear(string ciudad)
    {
        Ciudad c= await ciudadServicio.crear(ciudad);
        CiudadDTO vmCiudad = autoMapper.Map<CiudadDTO>(c);
        return StatusCode(StatusCodes.Status200OK, vmCiudad);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> eliminar(int id)
    {
        Ciudad eliminado =await ciudadServicio.eliminar(id);
        CiudadDTO vmCiudad = autoMapper.Map<CiudadDTO>(eliminado);
        return StatusCode(StatusCodes.Status200OK, vmCiudad);
    }


    [HttpPatch("editar")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> editar(CiudadDTO ciudad)
    {
        Ciudad actualizado = await ciudadServicio.editar(ciudad.ciudad, ciudad.idCiudad);
        CiudadDTO vmCiudad = autoMapper.Map<CiudadDTO>(actualizado);
        return StatusCode(StatusCodes.Status200OK, vmCiudad);

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System.Web.Http.Results;



namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CasasController:ControllerBase
{

    private readonly ICasaServicio casaServicio;

    private readonly IMapper autoMapper;
    

    public CasasController(ICasaServicio casaServicio, IMapper autoMapper)
    {
        this.casaServicio = casaServicio;

        this.autoMapper = autoMapper;


    }

    [HttpGet("listaCasas")]
    public async Task<IActionResult> listaCasas()
    {
        var casas = await casaServicio.lista();
        
        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> casaPorId(int id)
    {
        var casa = await casaServicio.obtenerPorId(id);
        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }


    [HttpGet("{idCiudad}casaPorCiudad")]
    public async Task<IActionResult> listaCasa_Ciudad(int idCiudad)
    {
        var casas =await casaServicio.obtenerCasaPorCiudad(idCiudad);
        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }

    [HttpPost("filtrar")]
    public async Task<IActionResult> listaCasasFiltradas([FromBody] BusquedaCasaDTO busquedaCasaDTO)
    {
        var casas = await casaServicio.obtenerCasasFiltradas(busquedaCasaDTO);
        List<CasaDTO> vmCasas = autoMapper.Map<List<CasaDTO>>(casas);
        return StatusCode(StatusCodes.Status200OK, vmCasas);
    }


    [HttpPost("crear")]
    public async Task<IActionResult> crear([FromBody]CrearCasaDTO casaDTO)
    {
        Casa c = autoMapper.Map<Casa>(casaDTO);
        Casa casa = await casaServicio.crear(c, casaDTO.caracteristicasDTO);

        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }

    [HttpPatch("editar")]
    public async Task<IActionResult> editar([FromBody]EditarCasaDTO casaDTO)
    {
        CaracteristicaDTO caracteristica = autoMapper.Map<CaracteristicaDTO>(casaDTO.caracteristicas);
        Casa casa = await casaServicio.editar(casaDTO, caracteristica);

        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> borrar(int id)
    {
        Casa casa = await casaServicio.eliminar(id);
        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;



namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CasaPendienteController:ControllerBase
{
    private readonly ICasaPendienteServicio casaServicio;

    private readonly IMapper autoMapper;


    public CasaPendienteController(ICasaPendienteServicio casaServicio, IMapper autoMapper)
    {
        this.casaServicio = casaServicio;

        this.autoMapper = autoMapper;
    }

    [HttpGet("listaCasas")]
    public async Task<IActionResult> listaCasas()
    {
        var casas = await casaServicio.listaPendientes();

        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }

    [HttpPost("crear")]
    public async Task<IActionResult> crear([FromBody] CrearCasaDTO casaDTO)
    {
        Casa c = autoMapper.Map<Casa>(casaDTO);
        CasaPendiente casa = await casaServicio.crear(c, casaDTO.caracteristicasDTO);

        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }

    [HttpPost("publicar{id}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> publicar(int id)
    {
        Casa casa = await casaServicio.publicar(id);


        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }

    [HttpPatch("editar")]
    public async Task<IActionResult> editar([FromBody] EditarCasaDTO casaDTO)
    {
        CaracteristicaDTO caracteristica = autoMapper.Map<CaracteristicaDTO>(casaDTO.caracteristicas);
        CasaPendiente casa = await casaServicio.editar(casaDTO, caracteristica);

        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> cancelar(int id)
    {
        CasaPendiente casa = await casaServicio.cancelarSolicitud(id);
        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }
}

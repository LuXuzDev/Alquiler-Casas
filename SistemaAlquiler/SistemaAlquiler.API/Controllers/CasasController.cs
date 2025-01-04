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
    private readonly IFotoServicio fotoServicio;
    private readonly IMapper autoMapper;
    

    public CasasController(ICasaServicio casaServicio, IMapper autoMapper,IFotoServicio fotoServicio)
    {
        this.casaServicio = casaServicio;
        this.fotoServicio = fotoServicio;
        this.autoMapper = autoMapper;
    }

    [HttpGet("listaCasas")]
    public async Task<IActionResult> listaCasas()
    {
        var casas = await casaServicio.lista();
        
        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        await fotoServicio.llenarDTOs(vmLista);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }

    [HttpGet("listaCasasPendientes")]
    public async Task<IActionResult> listaCasasPendientes()
    {
        var casas = await casaServicio.listaPendientes();

        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        await fotoServicio.llenarDTOs(vmLista);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> casaPorId(int id)
    {
        var casa = await casaServicio.obtenerPorId(id);
        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        await fotoServicio.llenarDTO(vmCasa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }


    [HttpGet("{idCiudad}casaPorCiudad")]
    public async Task<IActionResult> listaCasa_Ciudad(int idCiudad)
    {
        var casas =await casaServicio.obtenerCasaPorCiudad(idCiudad);
        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        await fotoServicio.llenarDTOs(vmLista);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }

    [HttpPost("filtrar")]
    public async Task<IActionResult> listaCasasFiltradas([FromBody] BusquedaCasaDTO busquedaCasaDTO)
    {
        var casas = await casaServicio.obtenerCasasFiltradas(busquedaCasaDTO);
        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        await fotoServicio.llenarDTOs(vmLista);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }

    [HttpPost("filtrarInicial")]
    public async Task<IActionResult> listaCasasFiltradasInicial([FromBody] FiltradoInicialDTO filtrado)
    {
        var casas = await casaServicio.filtradoInicial(filtrado);
        List<CasaDTO> vmLista = autoMapper.Map<List<CasaDTO>>(casas);
        await fotoServicio.llenarDTOs(vmLista);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }


    [HttpPost("crear")]
    public async Task<IActionResult> crear([FromForm]CrearCasaDTO casaDTO,List<IFormFile> fotos )
    {
        Casa c = autoMapper.Map<Casa>(casaDTO);
        Casa casa = await casaServicio.crear(casaDTO,fotos);

        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        await fotoServicio.llenarDTO(vmCasa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }

    [HttpPatch("editar")]
    public async Task<IActionResult> editar([FromBody]EditarCasaDTO casaDTO)
    {
        CaracteristicaDTO caracteristica = autoMapper.Map<CaracteristicaDTO>(casaDTO.caracteristicas);
        Casa casa = await casaServicio.editar(casaDTO, caracteristica);

        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        await fotoServicio.llenarDTO(vmCasa);
        return StatusCode(StatusCodes.Status200OK, vmCasa);
    }

    [HttpPatch("publicar")]
    public async Task<IActionResult> publicar(int idCasa)
    {
        
        Casa casa = await casaServicio.publicarCasa(idCasa);

        CasaDTO vmCasa = autoMapper.Map<CasaDTO>(casa);
        await fotoServicio.llenarDTO(vmCasa);
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

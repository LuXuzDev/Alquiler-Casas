using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.API.DTOs;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System.Web.Http.Results;



namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("casas/")]
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

    [HttpGet("{id}casaID")]
    public async Task<IActionResult> casaPorId(int id)
    {
        var casa = await casaServicio.obtenerPorId(id);

        return StatusCode(StatusCodes.Status200OK,casa);
    }


    [HttpGet("{idCiudad}filtroCiudad")]
    public async Task<IActionResult> listaCasa_Ciudad(int idCiudad)
    {
        var casas =await casaServicio.obtenerCasaPorCiudad(idCiudad);
        return StatusCode(StatusCodes.Status200OK, casas);
    }

    [HttpGet("filtrar")]
    public async Task<IActionResult> listaCasasFiltradas(int? cantMaxPersonas, int? cantHabitaciones, int? cantBanos,
        int? cantCuartos, Boolean? cocina, Boolean? terraza_balcon, Boolean? barbacoa, Boolean? garaje,
        Boolean? piscina, Boolean? gimnasio, Boolean? lavadora_secadora, Boolean? tv, Boolean? permiteMenores,
        Boolean? permiteFumar, Boolean? permiteMascotas, Boolean? wifi, Boolean? aguaCaliente, Boolean? climatizada,
        double? precioNoche,double? precioMes,double? areaTotal 
        )
    {
        var casas = casaServicio.obtenerCasasFiltradas(cantMaxPersonas, cantHabitaciones, cantBanos, cantCuartos, cocina, terraza_balcon, barbacoa,
            garaje, piscina, gimnasio, lavadora_secadora, tv, permiteMenores, permiteFumar, permiteMascotas, wifi, aguaCaliente,
            climatizada, precioNoche, precioMes, areaTotal);
        return StatusCode(StatusCodes.Status200OK, casas);
    }


    [HttpPost("crear")]
    public async Task<IActionResult> crear(int idCaracteristica, double precioNoche, double precioMes,
        double areaTotal, string descripcion,int idCiudad, int idUsuario)
    {
        var casa =await casaServicio.crear(idCaracteristica,precioNoche, precioMes,
            areaTotal, descripcion, idCiudad,idUsuario);
        return StatusCode(StatusCodes.Status200OK, casa);
    }
}

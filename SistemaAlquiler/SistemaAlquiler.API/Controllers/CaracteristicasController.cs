using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;

namespace SistemaAlquiler.API.Controllers;

public class CaracteristicasController:ControllerBase
{
    private readonly ICaracteristicaServicio caracteristicaServicio;

    private readonly IMapper autoMapper;

    public CaracteristicasController(ICaracteristicaServicio caracteristicaServicio, IMapper autoMapper)
    {
        this.caracteristicaServicio = caracteristicaServicio;
        this.autoMapper = autoMapper;
    }


    [HttpGet("obtener")]
    public async Task<IActionResult> obtener(int idCaracteristica)
    {
        var caracteristica = await caracteristicaServicio.obtenerPorId(idCaracteristica);
        return StatusCode(StatusCodes.Status200OK, caracteristica);
    }

    [HttpPost("crear")]
    public async Task<IActionResult> crear(int cantMaxPersonas, int cantHabitaciones, int cantBanos,
       int cantCuartos, Boolean cocina, Boolean terraza_balcon, Boolean barbacoa, Boolean garaje,
       Boolean piscina, Boolean gimnasio, Boolean lavadora_secadora, Boolean tv, Boolean permiteMenores,
       Boolean permiteFumar, Boolean permiteMascotas, Boolean wifi, Boolean aguaCaliente, Boolean climatizada)
    {
        var caracteristica = await caracteristicaServicio.crear(cantMaxPersonas, cantHabitaciones, cantBanos, cantCuartos,
            cocina, terraza_balcon, barbacoa, garaje, piscina, gimnasio, lavadora_secadora, tv, permiteMenores,permiteFumar,
            permiteMascotas, wifi, aguaCaliente, climatizada);
        return StatusCode(StatusCodes.Status200OK, caracteristica);
    }

    [HttpDelete("eliminar")]
    public async Task<IActionResult> eliminar(int idCaracteristica)
    {
        var caracteristica = await caracteristicaServicio.eliminar(idCaracteristica);
        return StatusCode(StatusCodes.Status200OK, caracteristica);
    }
}

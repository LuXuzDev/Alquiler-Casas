using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class CaracteristicaServicio : ICaracteristicaServicio
{
    private readonly IRepositorioGenerico<Caracteristicas> repositorio;
    private readonly IValidadorServicio validadorServicio;

    public CaracteristicaServicio(IRepositorioGenerico<Caracteristicas> repositorio, IValidadorServicio validadorServicio)
    {
        this.repositorio = repositorio;
        this.validadorServicio = validadorServicio;
    }
    public async Task<Caracteristicas> crear(CrearCaracteristicasDTO caracteristicaDTO)
    {
        await validadorServicio.validarNumerosEnteros(1, 40, caracteristicaDTO.cantMaxPersonas, "La cantidad de personas es incorrecta");
        await validadorServicio.validarNumerosEnteros(1, 20, caracteristicaDTO.cantBanos, "La cantidad de baños es incorrecta");
        await validadorServicio.validarNumerosEnteros(1, 20, caracteristicaDTO.cantCuartos, "La cantidad de cuartos es incorrecta");
        await validadorServicio.validarNumerosEnteros(2, 20, caracteristicaDTO.cantHabitaciones, "La cantidad de habitaciones es incorrecta");

        if (caracteristicaDTO.cantHabitaciones < caracteristicaDTO.cantBanos + caracteristicaDTO.cantCuartos + 1)
            throw new TaskCanceledException("La cantidad de habitaciones es incorrecta");


        Caracteristicas caracteristicas = new Caracteristicas(caracteristicaDTO.cantMaxPersonas, caracteristicaDTO.cantHabitaciones,
            caracteristicaDTO.cantBanos, caracteristicaDTO.cantCuartos,
            caracteristicaDTO.cocina, caracteristicaDTO.terraza_balcon, caracteristicaDTO.barbacoa,
            caracteristicaDTO.garaje, caracteristicaDTO.piscina, caracteristicaDTO.gimnasio,
            caracteristicaDTO.lavadora_secadora, caracteristicaDTO.tv, caracteristicaDTO.permiteMenores,
            caracteristicaDTO.permiteFumar,caracteristicaDTO.permiteMascotas, caracteristicaDTO.wifi,
            caracteristicaDTO.aguaCaliente, caracteristicaDTO.climatizada);
        await repositorio.crear(caracteristicas);
        return caracteristicas;
    }

    public async Task<Caracteristicas> editar(int idCaracteristica,CaracteristicaDTO caracteristicaDTO)
    {
        Caracteristicas caracteristica = await validadorServicio.existeCaracteristica(idCaracteristica, "No existe esa caracteristica");

        if (caracteristicaDTO.cantHabitaciones < caracteristicaDTO.cantBanos + caracteristicaDTO.cantCuartos + 1)
            throw new TaskCanceledException("La cantidad de habitaciones es incorrecta");
        try
        {
            #region Modificando valores
            if (caracteristicaDTO.cantMaxPersonas.HasValue)
            {
                await validadorServicio.validarNumerosEnteros(1, 40,(int) caracteristicaDTO.cantMaxPersonas, "La cantidad de personas es incorrecta");
                caracteristica.cantMaxPersonas = (int)caracteristicaDTO.cantMaxPersonas;
            }
                

            if(caracteristicaDTO.cantHabitaciones.HasValue)
            {
                await validadorServicio.validarNumerosEnteros(1, 20, (int)caracteristicaDTO.cantBanos, "La cantidad de habitaciones es incorrecta");
                caracteristica.cantHabitaciones = (int)caracteristicaDTO.cantHabitaciones;
            }
                

            if(caracteristicaDTO.cantBanos.HasValue)
            {
                await validadorServicio.validarNumerosEnteros(1, 20,(int) caracteristicaDTO.cantBanos, "La cantidad de baños es incorrecta");
                caracteristica.cantBanos = (int)caracteristicaDTO.cantBanos;
            }

            if(caracteristicaDTO.cantCuartos.HasValue)
            {
                await validadorServicio.validarNumerosEnteros(1, 20, (int)caracteristicaDTO.cantBanos, "La cantidad de cuartos es incorrecta");
                caracteristica.cantCuartos = (int)caracteristicaDTO.cantCuartos;
            }

            if(caracteristicaDTO.cocina.HasValue)
                caracteristica.cocina = caracteristicaDTO.cocina.Value;

            if (caracteristicaDTO.terraza_balcon.HasValue)
                caracteristica.terraza_balcon = caracteristicaDTO.terraza_balcon.Value;

            if(caracteristicaDTO.barbacoa.HasValue)
                caracteristica.barbacoa = caracteristicaDTO.barbacoa.Value;

            if(caracteristicaDTO.garaje.HasValue)
                caracteristica.garaje = caracteristicaDTO.garaje.Value;

            if(caracteristicaDTO.piscina.HasValue)
                caracteristica.piscina = caracteristicaDTO.piscina.Value;

            if(caracteristicaDTO.gimnasio.HasValue)
                caracteristica.gimnasio = caracteristicaDTO.gimnasio.Value;

            if(caracteristicaDTO.lavadora_secadora.HasValue)
                caracteristica.lavadora_secadora = caracteristicaDTO.lavadora_secadora.Value;

            if(caracteristicaDTO.tv.HasValue)
                caracteristica.tv = caracteristicaDTO.tv.Value;

            if(caracteristicaDTO.permiteMenores.HasValue)
                caracteristica.permiteMenores = caracteristicaDTO.permiteMenores.Value;

            if(caracteristicaDTO.permiteFumar.HasValue)
                caracteristica.permiteFumar = caracteristicaDTO.permiteFumar.Value;

            if(caracteristicaDTO.permiteMascotas.HasValue)
                caracteristica.permiteMascotas = caracteristicaDTO.permiteMascotas.Value;

            if(caracteristicaDTO.wifi.HasValue)
                caracteristica.wifi = caracteristicaDTO.wifi.Value;

            if(caracteristicaDTO.aguaCaliente.HasValue)
                caracteristica.aguaCaliente = caracteristicaDTO.aguaCaliente.Value;

            if(caracteristicaDTO.climatizada.HasValue)
                caracteristica.climatizada = caracteristicaDTO.climatizada.Value;
            #endregion

            bool editado =await repositorio.editar(caracteristica);
            return caracteristica;
            
        }
        catch (Exception ex)
        {
            throw;
        }
            
    }

    public async Task<Caracteristicas> eliminar(int idCaracteristica)
    {
        Caracteristicas caracteristica =await validadorServicio.existeCaracteristica(idCaracteristica, "No existe esa caracteristica");
        repositorio.eliminar(caracteristica);
        return caracteristica;
    }

    public async Task<Caracteristicas> obtenerPorId(int idCaracteristica)
    {
        Caracteristicas caracteristica = await validadorServicio.existeCaracteristica(idCaracteristica, "No existe esa caracteristica");
        return caracteristica;
    }
}

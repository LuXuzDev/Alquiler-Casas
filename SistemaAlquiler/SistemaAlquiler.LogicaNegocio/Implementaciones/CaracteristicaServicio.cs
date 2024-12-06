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

    public CaracteristicaServicio(IRepositorioGenerico<Caracteristicas> repositorio)

    {
        this.repositorio = repositorio;
    }
    public async Task<Caracteristicas> crear(CrearCaracteristicasDTO caracteristicaDTO)
    {
        if(caracteristicaDTO.cantHabitaciones < caracteristicaDTO.cantBanos + caracteristicaDTO.cantCuartos + 1)
            throw new TaskCanceledException("Error en los datos");

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
        var caracteristica = await repositorio.obtenerTodos(u => u.idCaracteristicas == idCaracteristica);
        if (caracteristica == null)
            throw new TaskCanceledException("No existe esa caracteristica");
        if (caracteristicaDTO.cantHabitaciones < caracteristicaDTO.cantBanos + caracteristicaDTO.cantCuartos + 1)
            throw new TaskCanceledException("Error en los datos");
        try
        {
            #region Modificando valores
            if (caracteristicaDTO.cantMaxPersonas.HasValue)
                caracteristica.cantMaxPersonas = (int)caracteristicaDTO.cantMaxPersonas;

            if(caracteristicaDTO.cantHabitaciones.HasValue)
                caracteristica.cantHabitaciones = (int)caracteristicaDTO.cantHabitaciones;

            if(caracteristicaDTO.cantBanos.HasValue)
                caracteristica.cantBanos = (int)caracteristicaDTO.cantBanos;

            if(caracteristicaDTO.cantCuartos.HasValue)
                caracteristica.cantCuartos = (int)caracteristicaDTO.cantCuartos;

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
        var caracteristica = await repositorio.obtenerTodos(u => u.idCaracteristicas == idCaracteristica);
        if(caracteristica==null)
            throw new TaskCanceledException("No existe esa caracteristica");
        repositorio.eliminar(caracteristica);
        return caracteristica;
    }

    public async Task<Caracteristicas> obtenerPorId(int idCaracteristica)
    {
        var caracteristica = await repositorio.obtenerTodos(u=> u.idCaracteristicas==idCaracteristica);
        return caracteristica;
    }
}

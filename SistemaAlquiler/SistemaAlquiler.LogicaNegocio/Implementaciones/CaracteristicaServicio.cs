using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
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
    public async Task<Caracteristicas> crear(int cantMaxPersonas, int cantHabitaciones, int cantBanos,
        int cantCuartos, bool cocina, bool terraza_balcon,
        bool barbacoa, bool garaje, bool piscina, bool gimnasio, bool lavadora_secadora, bool tv,
        bool permiteMenores, bool permiteFumar, bool permiteMascotas, bool wifi,
        bool aguaCaliente, bool climatizada)
    {
        if(cantHabitaciones< cantBanos+cantCuartos+1)
            throw new TaskCanceledException("Error en los datos");

        Caracteristicas caracteristicas = new Caracteristicas(cantMaxPersonas, cantHabitaciones,cantBanos, cantCuartos,
            cocina, terraza_balcon, barbacoa, garaje, piscina, gimnasio,lavadora_secadora, tv, permiteMenores, permiteFumar,
            permiteMascotas, wifi, aguaCaliente, climatizada);
        await repositorio.crear(caracteristicas);
        return caracteristicas;
    }

    public async Task<Caracteristicas> editar(int idCaracteristica, int? cantMaxPersonas, int? cantHabitaciones, int? cantBanos,
        int? cantCuartos, bool? cocina, bool? terraza_balcon, bool? barbacoa, bool? garaje, bool? piscina,
        bool? gimnasio, bool? lavadora_secadora, bool? tv, bool? permiteMenores, bool? permiteFumar,
        bool? permiteMascotas, bool? wifi, bool? aguaCaliente, bool? climatizada)
    {
        var caracteristica = await repositorio.obtenerTodos(u => u.idCaracteristicas == idCaracteristica);
        if (caracteristica == null)
            throw new TaskCanceledException("No existe esa caracteristica");
        if (cantHabitaciones < cantBanos + cantCuartos + 1)
            throw new TaskCanceledException("Error en los datos");
        try
        {
            #region Modificando valores
            if (cantMaxPersonas.HasValue)
                caracteristica.cantMaxPersonas = (int)cantMaxPersonas;

            if(cantHabitaciones.HasValue)
                caracteristica.cantHabitaciones = (int)cantHabitaciones;

            if(cantBanos.HasValue)
                caracteristica.cantBanos = (int)cantBanos;

            if(cantCuartos.HasValue)
                caracteristica.cantCuartos = (int)cantCuartos;

            if(cocina.HasValue)
                caracteristica.cocina = cocina.Value;

            if (terraza_balcon.HasValue)
                caracteristica.terraza_balcon = terraza_balcon.Value;

            if(barbacoa.HasValue)
                caracteristica.barbacoa = barbacoa.Value;

            if(garaje.HasValue)
                caracteristica.garaje = garaje.Value;

            if(piscina.HasValue)
                caracteristica.piscina = piscina.Value;

            if(gimnasio.HasValue)
                caracteristica.gimnasio = gimnasio.Value;

            if(lavadora_secadora.HasValue)
                caracteristica.lavadora_secadora = lavadora_secadora.Value;

            if(tv.HasValue)
                caracteristica.tv = tv.Value;

            if(permiteMenores.HasValue)
                caracteristica.permiteMenores = permiteMenores.Value;

            if(permiteFumar.HasValue)
                caracteristica.permiteFumar = permiteFumar.Value;

            if(permiteMascotas.HasValue)
                caracteristica.permiteMascotas = permiteMascotas.Value;

            if(wifi.HasValue)
                caracteristica.wifi = wifi.Value;

            if(aguaCaliente.HasValue)
                caracteristica.aguaCaliente = aguaCaliente.Value;

            if(climatizada.HasValue)
                caracteristica.climatizada = climatizada.Value;
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

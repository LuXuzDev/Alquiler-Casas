using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class CasaServicio : ICasaServicio
{
    private readonly IRepositorioGenerico<Casa> repositorio;
    private readonly IRepositorioGenerico<CasaPendiente> pendientes;
    private readonly ICaracteristicaServicio caracteristicaServicio;

    public CasaServicio(IRepositorioGenerico<Casa> repositorio, ICaracteristicaServicio caracteristicaServicio, IRepositorioGenerico<CasaPendiente> pendientes)
    {
        this.repositorio = repositorio;
        this.caracteristicaServicio = caracteristicaServicio;
        this.pendientes = pendientes;
    }



    public async Task<Casa> crear(Casa casa,CrearCaracteristicasDTO caracteristicas)
    {
        var caracteristica = await caracteristicaServicio.crear(caracteristicas);
        Casa casaCreada = null;
        try
        {
            casa.idCaracteristica = caracteristica.idCaracteristicas;
            casaCreada = await repositorio.crear(casa);
        }
        catch (Exception ex)
        {
            caracteristicaServicio.eliminar(caracteristica.idCaracteristicas);
            throw ex;
        }
        
        return casaCreada;

    }

    public async Task<Casa> editar(EditarCasaDTO casa,CaracteristicaDTO caracteristica)
    {
        var c = await repositorio.obtener(u=> u.idCasa == casa.idCasa);
        Casa casaEditada = c.FirstOrDefault();
        if(casaEditada==null)
           throw new TaskCanceledException("La casa no existe");

        caracteristicaServicio.editar(casa.idCaracteristica, caracteristica);
        casaEditada.areaTotal = casa.areaTotal;
        casaEditada.idCiudad = casa.idCiudad;
        casaEditada.descripcion = casa.descripcion;
        casaEditada.precioNoche = casa.precioNoche;
        casaEditada.precioMes = casa.precioMes;
        repositorio.editar(casaEditada);
        return casaEditada;

    }

    public async Task<Casa> eliminar(int idCasa)
    {
        Casa casa= await obtenerPorId(idCasa);
        caracteristicaServicio.eliminar(casa.idCaracteristica);
        repositorio.eliminar(casa);
        return casa;
    }

    public async Task<List<Casa>> lista()
    {
        IQueryable<Casa> consulta = await repositorio.obtener(u => u.idUsuario != null && u.idCiudad != null, [u => u.usuario,u=> u.caracteristicas, u=> u.ciudad]);
        List<Casa> casas = consulta.ToList();
        return casas;
    }


    public async Task<List<Casa>> obtenerCasaPorCiudad(int idCiudad)
    {
        IQueryable<Casa> consulta = await repositorio.obtener(u => u.idCiudad == idCiudad && u.idUsuario != null);
        List<Casa> casas = consulta.ToList();
        return casas;
    }

    public async Task<List<Casa>> obtenerCasasFiltradas(BusquedaCasaDTO busquedaCasaDTO)
    {
        IQueryable<Casa> casas = await repositorio.obtener(u=>u.idUsuario != null && u.idCiudad != null, [u => u.usuario, u => u.caracteristicas, u => u.ciudad]);


        #region Filtrado
        if (busquedaCasaDTO.cantMaxPersonas.HasValue)
            casas = casas.Where(c => c.caracteristicas.cantMaxPersonas >= busquedaCasaDTO.cantMaxPersonas.Value);

        if (busquedaCasaDTO.cantHabitaciones.HasValue)
            casas = casas.Where(c => c.caracteristicas.cantHabitaciones >= busquedaCasaDTO.cantHabitaciones.Value);

        if (busquedaCasaDTO.cantBanos.HasValue)
            casas = casas.Where(c => c.caracteristicas.cantBanos >= busquedaCasaDTO.cantBanos.Value);

        if (busquedaCasaDTO.cantCuartos.HasValue)
            casas = casas.Where(c => c.caracteristicas.cantCuartos >= busquedaCasaDTO.cantCuartos.Value);

        if (busquedaCasaDTO.cocina.HasValue)
            casas = casas.Where(c => c.caracteristicas.cocina == busquedaCasaDTO.cocina.Value);

        if (busquedaCasaDTO.terraza_balcon.HasValue)
            casas = casas.Where(c => c.caracteristicas.terraza_balcon == busquedaCasaDTO.terraza_balcon.Value);

        if (busquedaCasaDTO.barbacoa.HasValue)
            casas = casas.Where(c => c.caracteristicas.barbacoa == busquedaCasaDTO.barbacoa.Value);

        if (busquedaCasaDTO.garaje.HasValue)
            casas = casas.Where(c => c.caracteristicas.garaje == busquedaCasaDTO.garaje.Value);

        if (busquedaCasaDTO.piscina.HasValue)
            casas = casas.Where(c => c.caracteristicas.piscina == busquedaCasaDTO.piscina.Value);

        if (busquedaCasaDTO.gimnasio.HasValue)
            casas = casas.Where(c => c.caracteristicas.gimnasio == busquedaCasaDTO.gimnasio.Value);

        if (busquedaCasaDTO.lavadora_secadora.HasValue)
            casas = casas.Where(c => c.caracteristicas.lavadora_secadora == busquedaCasaDTO.lavadora_secadora.Value);

        if (busquedaCasaDTO.tv.HasValue)
            casas = casas.Where(c => c.caracteristicas.tv == busquedaCasaDTO.tv.Value);

        if (busquedaCasaDTO.permiteMenores.HasValue)
            casas = casas.Where(c => c.caracteristicas.permiteMenores == busquedaCasaDTO.permiteMenores.Value);

        if (busquedaCasaDTO.permiteFumar.HasValue)
            casas = casas.Where(c => c.caracteristicas.permiteFumar == busquedaCasaDTO.permiteFumar.Value);

        if (busquedaCasaDTO.permiteMascotas.HasValue)
            casas = casas.Where(c => c.caracteristicas.permiteMascotas == busquedaCasaDTO.permiteMascotas.Value);

        if (busquedaCasaDTO.wifi.HasValue)
            casas = casas.Where(c => c.caracteristicas.wifi == busquedaCasaDTO.wifi.Value);

        if (busquedaCasaDTO.aguaCaliente.HasValue)
            casas = casas.Where(c => c.caracteristicas.aguaCaliente == busquedaCasaDTO.aguaCaliente.Value);

        if (busquedaCasaDTO.climatizada.HasValue)
            casas = casas.Where(c => c.caracteristicas.climatizada == busquedaCasaDTO.climatizada.Value);

        if (busquedaCasaDTO.precioMes.HasValue)
            casas = casas.Where(c => c.precioMes <= busquedaCasaDTO.precioMes.Value);

        if (busquedaCasaDTO.precioNoche.HasValue)
            casas = casas.Where(c => c.precioNoche <= busquedaCasaDTO.precioNoche.Value);

        if (busquedaCasaDTO.areaTotal.HasValue)
            casas = casas.Where(c => c.areaTotal >= busquedaCasaDTO.areaTotal.Value);

        #endregion


        List<Casa> casasFiltradas = casas.ToList();
        return casasFiltradas;
    }

    public async Task<Casa> obtenerPorId(int idCasa)
    {
        var casa = await repositorio.obtenerTodos(u=> u.idCasa==idCasa);
        return casa;
    }

    
}

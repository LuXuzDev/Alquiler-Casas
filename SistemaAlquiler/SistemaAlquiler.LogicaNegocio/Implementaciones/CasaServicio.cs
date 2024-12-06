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
    private readonly ICaracteristicaServicio caracteristicaServicio;

    public CasaServicio(IRepositorioGenerico<Casa> repositorio, ICaracteristicaServicio caracteristicaServicio)
    {
        this.repositorio = repositorio;
        this.caracteristicaServicio = caracteristicaServicio;
    }
    
    public async Task<Casa> crear(Casa casa,CrearCaracteristicasDTO caracteristicas)
    {
        var caracteristica = await caracteristicaServicio.crear(caracteristicas);
        try
        {
            casa.idCaracteristica = caracteristica.idCaracteristicas;
            Casa casaCreada = await repositorio.crear(casa);
        }
        catch (Exception ex)
        {
            caracteristicaServicio.eliminar(caracteristica.idCaracteristicas);
            throw ex;
        }
        
        return casa;

    }

    public Task<Casa> editar(Casa casa)
    {
        throw new NotImplementedException();
    }

    public Task<Casa> eliminar(int idCasa)
    {
        throw new NotImplementedException();
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

        List<Casa> casasFiltradas = casas.ToList();
        for(int i= 0; i < casasFiltradas.Count(); i++)
        {
            Casa c = casasFiltradas[i];

            if (busquedaCasaDTO.cantMaxPersonas.HasValue)
                if(c.caracteristicas.cantMaxPersonas < busquedaCasaDTO.cantMaxPersonas)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.cantHabitaciones.HasValue)
                if (c.caracteristicas.cantHabitaciones < busquedaCasaDTO.cantHabitaciones)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.cantBanos.HasValue)
                if (c.caracteristicas.cantBanos < busquedaCasaDTO.cantBanos)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.cantCuartos.HasValue)
                if (c.caracteristicas.cantCuartos < busquedaCasaDTO.cantCuartos)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.cocina.HasValue)
                if (c.caracteristicas.cocina != busquedaCasaDTO.cocina)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.terraza_balcon.HasValue)
                if (c.caracteristicas.terraza_balcon != busquedaCasaDTO.terraza_balcon)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.barbacoa.HasValue)
                if (c.caracteristicas.barbacoa != busquedaCasaDTO.barbacoa)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.garaje.HasValue)
                if (c.caracteristicas.garaje != busquedaCasaDTO.garaje)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.piscina.HasValue)
                if (c.caracteristicas.piscina != busquedaCasaDTO.piscina)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.gimnasio.HasValue)
                if (c.caracteristicas.gimnasio != busquedaCasaDTO.gimnasio)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.lavadora_secadora.HasValue)
                if (c.caracteristicas.lavadora_secadora != busquedaCasaDTO.lavadora_secadora)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.tv.HasValue)
                if (c.caracteristicas.tv != busquedaCasaDTO.tv)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.permiteMenores.HasValue)
                if (c.caracteristicas.permiteMenores != busquedaCasaDTO.permiteMenores)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.permiteFumar.HasValue)
                if (c.caracteristicas.permiteFumar != busquedaCasaDTO.permiteFumar)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.permiteMascotas.HasValue)
                if (c.caracteristicas.permiteMascotas != busquedaCasaDTO.permiteMascotas)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.wifi.HasValue)
                if (c.caracteristicas.wifi != busquedaCasaDTO.wifi)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.aguaCaliente.HasValue)
                if (c.caracteristicas.aguaCaliente != busquedaCasaDTO.aguaCaliente)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.climatizada.HasValue)
                if (c.caracteristicas.climatizada != busquedaCasaDTO.climatizada)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.precioNoche.HasValue)
                if (c.precioNoche < busquedaCasaDTO.precioNoche)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.precioMes.HasValue)
                if (c.precioMes < busquedaCasaDTO.precioMes)
                    casasFiltradas.Remove(c);

            if (busquedaCasaDTO.areaTotal.HasValue)
                if (c.areaTotal < busquedaCasaDTO.areaTotal)
                    casasFiltradas.Remove(c);
        }

        #endregion
        
        return casasFiltradas;
    }

    public Task<Casa> obtenerPorId(int idCasa)
    {
        var casa = repositorio.obtenerTodos(u=> u.idCasa==idCasa);
        return casa;
    }
}

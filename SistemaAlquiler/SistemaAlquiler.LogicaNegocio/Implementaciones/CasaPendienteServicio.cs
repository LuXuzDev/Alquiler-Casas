using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class CasaPendienteServicio: ICasaPendienteServicio
{
    private readonly IRepositorioGenerico<Casa> repositorio;
    private readonly IRepositorioGenerico<CasaPendiente> pendientes;
    private readonly ICaracteristicaServicio caracteristicaServicio;

    public CasaPendienteServicio(ICaracteristicaServicio caracteristicaServicio, IRepositorioGenerico<CasaPendiente> pendientes, IRepositorioGenerico<Casa> repositorio)
    {
        this.caracteristicaServicio = caracteristicaServicio;
        this.pendientes = pendientes;
        this.repositorio = repositorio;
    }

    public async Task<CasaPendiente> cancelarSolicitud(int id)
    {
        var consulta = await pendientes.obtener(u => u.idCasa == id);
        CasaPendiente casaPendiente = consulta.FirstOrDefault();
        try
        {
            if (casaPendiente == null)
                throw new TaskCanceledException("No existe esa solicitud");
            await pendientes.eliminar(casaPendiente);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return casaPendiente;
    }

    public async Task<CasaPendiente> crear(Casa casa, CrearCaracteristicasDTO caracteristicas)
    {
        var caracteristica = await caracteristicaServicio.crear(caracteristicas);
        CasaPendiente casaCreada = null;
        try
        {
            casa.idCaracteristica = caracteristica.idCaracteristicas;
            CasaPendiente casaPendiente = new CasaPendiente(casa);
            casaCreada = await pendientes.crear(casaPendiente);
        }
        catch (Exception ex)
        {
            caracteristicaServicio.eliminar(caracteristica.idCaracteristicas);
            throw ex;
        }
        return casaCreada;
    }

    public async Task<CasaPendiente> editar(EditarCasaDTO casa, CaracteristicaDTO caracteristica)
    {
        var c = await pendientes.obtener(u => u.idCasa == casa.idCasa);
        CasaPendiente casaEditada = c.FirstOrDefault();
        if (casaEditada == null)
            throw new TaskCanceledException("La casa no existe");

        caracteristicaServicio.editar(casa.idCaracteristica, caracteristica);
        casaEditada.areaTotal = casa.areaTotal;
        casaEditada.idCiudad = casa.idCiudad;
        casaEditada.descripcion = casa.descripcion;
        casaEditada.precioNoche = casa.precioNoche;
        casaEditada.precioMes = casa.precioMes;
        pendientes.editar(casaEditada);
        return casaEditada;

    }

    public async Task<List<CasaPendiente>> listaPendientes()
    {
        IQueryable<CasaPendiente> consulta = await pendientes.obtener(u => u.idUsuario != null && u.idCiudad != null, [u => u.usuario, u => u.caracteristicas, u => u.ciudad]);
        List<CasaPendiente> casas = consulta.ToList();
        return casas;
    }

    public async Task<Casa> publicar(int id)
    {
        var consulta = await pendientes.obtener(u => u.idCasa == id);
        CasaPendiente casaPendiente = consulta.FirstOrDefault();
        Casa casaCreada = null;
        try
        {
            if (casaPendiente == null)
                throw new TaskCanceledException("No existe esa solicitud");
            Casa casa = new Casa(casaPendiente);
            casaCreada = await repositorio.crear(casa);
            await pendientes.eliminar(casaPendiente);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return casaCreada;

    }
}

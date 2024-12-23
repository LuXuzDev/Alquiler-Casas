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
    private readonly IValidadorServicio validadorServicio;
    public CasaPendienteServicio(ICaracteristicaServicio caracteristicaServicio, IRepositorioGenerico<CasaPendiente> pendientes,
        IRepositorioGenerico<Casa> repositorio,IValidadorServicio validadorServicio)
    {
        this.caracteristicaServicio = caracteristicaServicio;
        this.pendientes = pendientes;
        this.repositorio = repositorio;
        this.validadorServicio= validadorServicio;
    }

    public async Task<CasaPendiente> cancelarSolicitud(int id)
    {
        CasaPendiente casaPendiente = await validadorServicio.existeCasaPendiente(id, "La casa no existe");
        await pendientes.eliminar(casaPendiente);
        return casaPendiente;
    }

    public async Task<CasaPendiente> crear(Casa casa, CrearCaracteristicasDTO caracteristicas)
    {
        await validadorServicio.validarNumerosDouble(0,1000000,casa.precioNoche,"El precio de la noche no es correcto");
        await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioMes, "El precio del mes no es correcto");
        await validadorServicio.validarNumerosDouble(0, 10000, casa.areaTotal, "El área total no es correcta");
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
            await caracteristicaServicio.eliminar(caracteristica.idCaracteristicas);
            throw ex;
        }
        return casaCreada;
    }

    public async Task<CasaPendiente> editar(EditarCasaDTO casa, CaracteristicaDTO caracteristica)
    {
        await validadorServicio.existeCuidad(casa.idCiudad, "La cuidad no es correcta");
        CasaPendiente casaEditada = await validadorServicio.existeCasaPendiente(casa.idCasa, "La casa no existe");
        await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioNoche, "El precio de la noche no es correcto");
        await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioMes, "El precio del mes no es correcto");
        await validadorServicio.validarNumerosDouble(0, 10000, casa.areaTotal, "El área total no es correcta");


        await caracteristicaServicio.editar(casa.idCaracteristica, caracteristica);
        casaEditada.areaTotal = casa.areaTotal;
        casaEditada.idCiudad = casa.idCiudad;
        casaEditada.descripcion = casa.descripcion;
        casaEditada.precioNoche = casa.precioNoche;
        casaEditada.precioMes = casa.precioMes;
        await pendientes.editar(casaEditada);
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
        CasaPendiente casaPendiente = await validadorServicio.existeCasaPendiente(id, "La casa no existe");
        Casa casaCreada = null;
        try
        {
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

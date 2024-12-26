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
        if (casa.idCiudad.HasValue)
            await validadorServicio.existeCuidad(casa.idCiudad.Value, "La cuidad no es correcta");
        CasaPendiente casaEditada = await validadorServicio.existeCasaPendiente(casa.idCasa, "La casa no existe");

        if (casa.precioNoche.HasValue)
            await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioNoche.Value, "El precio de la noche no es correcto");

        if (casa.precioMes.HasValue)
            await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioMes.Value, "El precio del mes no es correcto");

        if (casa.areaTotal.HasValue)
            await validadorServicio.validarNumerosDouble(0, 10000, casa.areaTotal.Value, "El área total no es correcta");
        var c = await repositorio.obtener(u => u.idCasa == casa.idCasa);



        if (casa.caracteristicas != null)
            await caracteristicaServicio.editar(casaEditada.idCaracteristica, caracteristica);
        if (casa.areaTotal.HasValue)
            casaEditada.areaTotal = casa.areaTotal.Value;

        if (casa.idCiudad.HasValue)
            casaEditada.idCiudad = casa.idCiudad;

        if (casa.descripcion != null && !casa.descripcion.Equals(""))
            casaEditada.descripcion = casa.descripcion;

        if (casa.precioNoche.HasValue)
            casaEditada.precioNoche = casa.precioNoche.Value;

        if (casa.precioMes.HasValue)
            casaEditada.precioMes = casa.precioMes.Value;

        if (casa.nombre!=null && !casa.nombre.Equals(""))
            casaEditada.nombre = casa.nombre;

        if (casa.direccion!=null && !casa.direccion.Equals(""))
            casaEditada.direccion = casa.direccion;

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

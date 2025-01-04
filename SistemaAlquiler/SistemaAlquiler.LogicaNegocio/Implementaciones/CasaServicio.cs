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
    private readonly IRepositorioGenerico<Reservacion> reservacion;
    private readonly ICaracteristicaServicio caracteristicaServicio;
    private readonly IValidadorServicio validadorServicio;

    public CasaServicio(IRepositorioGenerico<Casa> repositorio, ICaracteristicaServicio caracteristicaServicio,
        IValidadorServicio validadorServicio,IRepositorioGenerico<Reservacion> reservacion)
    {
        this.repositorio = repositorio;
        this.caracteristicaServicio = caracteristicaServicio;
        this.validadorServicio= validadorServicio;
        this.reservacion = reservacion;
    }



    public async Task<Casa> crear(Casa casa,CrearCaracteristicasDTO caracteristicas)
    {
        await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioNoche, "El precio de la noche no es correcto");
        await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioMes, "El precio del mes no es correcto");
        await validadorServicio.validarNumerosDouble(0, 10000, casa.areaTotal, "El área total no es correcta");

        var caracteristica = await caracteristicaServicio.crear(caracteristicas);
        Casa casaCreada = null;
        try
        {
            casa.idCaracteristica = caracteristica.idCaracteristicas;
            casaCreada = await repositorio.crear(casa);
        }
        catch (Exception ex)
        {
            await caracteristicaServicio.eliminar(caracteristica.idCaracteristicas);
            throw ex;
        }
        return casaCreada;
    }

    public async Task<Casa> editar(EditarCasaDTO casa,CaracteristicaDTO caracteristica)
    {
        if(casa.idCiudad.HasValue) 
            await validadorServicio.existeCuidad(casa.idCiudad.Value, "La cuidad no es correcta");
        Casa casaEditada =await validadorServicio.existeCasa(casa.idCasa, "La casa no existe");

        if (casa.precioNoche.HasValue)
            await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioNoche.Value, "El precio de la noche no es correcto");

        if (casa.precioMes.HasValue)
            await validadorServicio.validarNumerosDouble(0, 1000000, casa.precioMes.Value, "El precio del mes no es correcto");

        if (casa.areaTotal.HasValue)
            await validadorServicio.validarNumerosDouble(0, 10000, casa.areaTotal.Value, "El área total no es correcta");
        var c = await repositorio.obtener(u=> u.idCasa == casa.idCasa);
        
        

        if(casa.caracteristicas!=null)
        await caracteristicaServicio.editar(casaEditada.idCaracteristica, caracteristica);
        if(casa.areaTotal.HasValue)
            casaEditada.areaTotal = casa.areaTotal.Value;

        if (casa.idCiudad.HasValue)
            casaEditada.idCiudad = casa.idCiudad;

        if (casa.descripcion != null && !casa.descripcion.Equals(""))
            casaEditada.descripcion = casa.descripcion;

        if (casa.precioNoche.HasValue)
            casaEditada.precioNoche = casa.precioNoche.Value;

        if (casa.precioMes.HasValue)
            casaEditada.precioMes = casa.precioMes.Value;

        if (casa.nombre != null && !casa.nombre.Equals(""))
            casaEditada.nombre = casa.nombre;

        if (casa.direccion != null && !casa.direccion.Equals(""))
            casaEditada.direccion = casa.direccion;

        await repositorio.editar(casaEditada);
        return casaEditada;

    }

    public async Task<Casa> eliminar(int idCasa)
    {
        Casa casa= await obtenerPorId(idCasa);
        await caracteristicaServicio.eliminar(casa.idCaracteristica);
        await repositorio.eliminar(casa);
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
        await validadorServicio.existeCuidad(idCiudad,"La cuidad no existe");
        IQueryable<Casa> consulta = await repositorio.obtener(u => u.idCiudad == idCiudad && u.idUsuario != null,[u => u.usuario, u => u.caracteristicas, u => u.ciudad]);
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


    public async Task<List<Casa>> filtradoInicial(FiltradoInicialDTO filtrado)
    {
        IQueryable<Casa> casas = await repositorio.obtener(u => u.idUsuario != null && u.idCiudad != null, 
            [u => u.usuario, u => u.caracteristicas, u => u.ciudad]);

        if (filtrado.cantMaxPersonas.HasValue)
        {
            casas = casas.Where(c => c.caracteristicas.cantMaxPersonas >= filtrado.cantMaxPersonas.Value);
        }

        if (!string.IsNullOrEmpty(filtrado.nombreCiudad))
        {
            casas = casas.Where(c => c.ciudad.ciudad.Equals(filtrado.nombreCiudad));
        }

        List<Casa> casasFiltradas = casas.ToList(); 

        if(filtrado.fechaEntrada.HasValue && filtrado.fechaSalida.HasValue)
        {
            var consulta = await reservacion.obtener(u => u.fechaEntrada.DayOfYear >= filtrado.fechaEntrada.Value.DayOfYear
                                                      && u.fechaSalida.DayOfYear <= filtrado.fechaSalida.Value.DayOfYear);

            List<Reservacion> reservaciones = consulta.ToList();

            // Filtrar las casas que están reservadas
            casasFiltradas = casasFiltradas
                .Where(casa => !reservaciones.Any(r => r.idCasa == casa.idCasa))
                .ToList();

        }
        return casasFiltradas;
    }

    public async Task<Casa> obtenerPorId(int idCasa)
    {
        await validadorServicio.existeCasa(idCasa, "No existe la casa");
        var consulta = await repositorio.obtener(u=> u.idCasa==idCasa, [u => u.usuario, u => u.caracteristicas, u => u.ciudad]);
        Casa casa = consulta.FirstOrDefault();
        return casa;
    }

    
}

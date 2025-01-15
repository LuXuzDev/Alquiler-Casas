using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class ReservacionServicio : IReservacionServicio
{
    private readonly IRepositorioGenerico<Reservacion> repositorio;
    private readonly IRepositorioGenerico<Casa> repositorioCasa;
    private readonly IRepositorioGenerico<Usuario> repositorioUsuario;
    private readonly IRepositorioGenerico<Caracteristicas> repositorioCaracteristicas;
    private readonly IValidadorServicio validadorServicio;

    public ReservacionServicio(IRepositorioGenerico<Reservacion> repositorio, IRepositorioGenerico<Casa> repositorioCasa,
        IRepositorioGenerico<Usuario> repositorioUsuario, IRepositorioGenerico<Caracteristicas> repositorioCaracteristicas,
        IValidadorServicio validadorServicio)
    {
        this.repositorio = repositorio;
        this.repositorioCasa = repositorioCasa;
        this.repositorioUsuario = repositorioUsuario;
        this.repositorioCaracteristicas = repositorioCaracteristicas;
        this.validadorServicio = validadorServicio;
    }

    public async Task<Reservacion> eliminar(int idReservacion)
    {
        try
        {
            Reservacion reservacion = await validadorServicio.existeReservacion(idReservacion, "La reservacion no existe");
            bool respuesta = await repositorio.eliminar(reservacion);
            if (!respuesta)
                throw new TaskCanceledException("Error al eliminar la reservacion");
            return reservacion;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Reservacion>> lista()
    {
        IQueryable<Reservacion> consulta = await repositorio.obtener();
        return (List<Reservacion>)consulta.ToList();
    }

    public async Task<List<Reservacion>> listaIdCasa(int idCasa)
    {
        IQueryable<Reservacion> consulta = await repositorio.obtener(u=> u.idCasa==idCasa);
        return (List<Reservacion>)consulta.ToList();
    }


    public async Task<Reservacion> obtenerPorId(int idReservacion)
    {
        Reservacion reservacion = await validadorServicio.existeReservacion(idReservacion, "La reservacion no existe");
        return reservacion;
    }


    private async Task<double> costoTotal(DateOnly fechaEntrada, DateOnly fechaSalida, int idCasa)
    {
        await validadorServicio.existeCasa(idCasa, "No existe la casa");
        var casa = await repositorioCasa.obtener(u => u.idCasa == idCasa);
        if(fechaEntrada.CompareTo(fechaSalida) < 0)
        {
            double costo = 0;
            int noches = fechaSalida.DayOfYear - fechaEntrada.DayOfYear;
            if (fechaEntrada.Month+1<=fechaSalida.Month && fechaEntrada.DayOfYear <= fechaSalida.DayOfYear-30)
                costo = casa.FirstOrDefault().precioMes;
            else
                costo = casa.FirstOrDefault().precioNoche;

            return costo * noches;
        }
        else
            throw new TaskCanceledException("La fecha de entrada no puede ser despues que la fecha de salida");
        
    }

    private async Task<bool> disponibilidadPorFecha(DateOnly fechaEntrada, DateOnly fechaSalida)
    {
        bool estaDisponible = true;
        int entrada = fechaEntrada.DayOfYear;
        int salida = fechaSalida.DayOfYear;

        var reservaciones = await repositorio.obtener(u => u.fechaEntrada.DayOfYear >= entrada && u.fechaSalida.DayOfYear <= salida);
        if(reservaciones.FirstOrDefault()!=null)
            estaDisponible = false;

        return estaDisponible;
    }


    public async Task<Reservacion> crear(int idUsuario, int idCasa, int cantPersonas, DateOnly fechaEntrada, DateOnly fechaSalida)
    {
        await validadorServicio.existeUsuario(idUsuario, "No existe el usuario");
        Casa casa = await validadorServicio.existeCasa(idCasa, "No existe la casa");
        int cantMaxima = casa.caracteristicas.cantMaxPersonas;
        await validadorServicio.validarNumerosEnteros(1, cantMaxima, cantPersonas, "La cantidad de personas es incorrecta");

        if(!await disponibilidadPorFecha(fechaEntrada, fechaSalida))
            throw new TaskCanceledException("Ya existe una reservacion en esa fecha");

        try
        {
            double costo = await costoTotal(fechaEntrada, fechaSalida, idCasa);
            Reservacion reservacion = new Reservacion(idUsuario, idCasa, cantPersonas, fechaEntrada, fechaSalida, costo);
            
            var reservacionCreada = await repositorio.crear(reservacion);
            return reservacionCreada;
        }
        catch(Exception ex)
        {
            throw new TaskCanceledException("Error en crear la reservacion.");
        }
    }
}

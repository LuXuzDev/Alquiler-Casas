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

    public ReservacionServicio(IRepositorioGenerico<Reservacion> repositorio, IRepositorioGenerico<Casa> repositorioCasa,
        IRepositorioGenerico<Usuario> repositorioUsuario, IRepositorioGenerico<Caracteristicas> repositorioCaracteristicas)
    {
        this.repositorio = repositorio;
        this.repositorioCasa = repositorioCasa;
        this.repositorioUsuario = repositorioUsuario;
        this.repositorioCaracteristicas = repositorioCaracteristicas;
    }

    public async Task<Reservacion> eliminar(int idReservacion)
    {
        try
        {
            var reservacionEncontrada = await repositorio.obtener(u => u.idReservacion == idReservacion);
            Reservacion reservacion = reservacionEncontrada.FirstOrDefault();

            if (reservacion == null)
                throw new TaskCanceledException("La reservacion no existe");


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

    

    public async Task<Reservacion> obtenerPorId(int idReservacion)
    {
        IQueryable<Reservacion> consulta = await repositorio.obtener(u=> u.idReservacion==idReservacion);
        Reservacion reservacion = consulta.FirstOrDefault();
        if(reservacion== null)
            throw new TaskCanceledException("La reservacion no existe");
        return reservacion;
    }


    private async Task<double> costoTotal(DateOnly fechaEntrada, DateOnly fechaSalida, int idCasa)
    {
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

    private async Task<bool> revisarCasa(int idCasa)
    {
        bool existen = true;
        var casa = await repositorioCasa.obtener(u => u.idCasa == idCasa);

        if(casa.FirstOrDefault() == null)
            throw new TaskCanceledException("La casa no existe");
        return existen;
    }

    private async Task<bool> revisarCantidadPersonas(int idCasa, int cantPersonas)
    {
        var casa = await repositorioCasa.obtener(u => u.idCasa == idCasa);
        bool existen = true;
        var caracteristicas = await repositorioCaracteristicas.obtener(u => u.idCaracteristicas == casa.FirstOrDefault().idCaracteristica);

        if (caracteristicas.FirstOrDefault().cantMaxPersonas<cantPersonas)
            throw new TaskCanceledException("La casa no tiene disponibilidad para tantas personas");

        return existen;
    }
    private async Task<bool> revisarUsuario(int idUsuario)
    {
        bool existen = true;
        var usuario = await repositorioUsuario.obtener(u => u.idUsuario == idUsuario);
        
        if (usuario.FirstOrDefault() == null)
            throw new TaskCanceledException("El usuario no existe");

        return existen;
    }

    public async Task<Reservacion> crear(int idUsuario, int idCasa, int cantPersonas, DateOnly fechaEntrada, DateOnly fechaSalida)
    {
        await revisarUsuario(idUsuario);
        await revisarCasa(idCasa);
        await revisarCantidadPersonas(idCasa,cantPersonas);

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

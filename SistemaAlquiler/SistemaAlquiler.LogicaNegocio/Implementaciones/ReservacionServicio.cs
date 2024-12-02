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

    public ReservacionServicio(IRepositorioGenerico<Reservacion> repositorio, IRepositorioGenerico<Casa> repositorioCasa)
    {
        this.repositorio = repositorio;
        this.repositorioCasa = repositorioCasa;
    }

    public async Task<Reservacion> eliminar(int idReservacion)
    {
        try
        {
            var reservacionEncontrada = await repositorio.obtener(u => u.idReservacion == idReservacion);

            if (reservacionEncontrada.FirstOrDefault() == null)
                throw new TaskCanceledException("La reservacion no existe");


            bool respuesta = await repositorio.eliminar(reservacionEncontrada.FirstOrDefault());
            if (!respuesta)
                throw new TaskCanceledException("Error al eliminar la reservacion");
            return (Reservacion)reservacionEncontrada;
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

    public async Task<List<Reservacion>> obtenerPorGestor(int idGestor)
    {
        IQueryable<Reservacion> consulta = await repositorio.obtener(u => u.idUsuario == idGestor);
        return (List<Reservacion>)consulta.ToList();
    }

    public async Task<Reservacion> obtenerPorId(int idReservacion)
    {
        IQueryable<Reservacion> consulta = await repositorio.obtener(u=> u.idReservacion==idReservacion);
        return (Reservacion)consulta;
    }

    public async Task<double> creara(int idUsuario, int idCasa, int cantPersonas, DateTime fechaEntrada, DateTime fechaSalida)
    {
        
        return await costoTotal(fechaEntrada, fechaSalida, idCasa);
    }

    private async Task<double> costoTotal(DateTime fechaEntrada, DateTime fechaSalida, int idCasa)
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

    Task<Reservacion> IReservacionServicio.crear(int idUsuario, int idCasa, int cantPersonas, DateTime fechaEntrada, DateTime fechaSalida)
    {
        throw new NotImplementedException();
    }
}

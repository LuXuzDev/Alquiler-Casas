using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface IReservacionServicio
{
    Task<List<Reservacion>> lista();
    Task<Reservacion> crear(int idUsuario, int idCasa, int cantPersonas,
        DateTime fechaEntrada, DateTime fechaSalida);
    Task<Reservacion> eliminar(int idReservacion);
    Task<Reservacion> obtenerPorId(int idReservacion);
    Task<List<Reservacion>> obtenerPorGestor(int idGestor);
    Task<double> creara(int idUsuario, int idCasa, int cantPersonas, DateTime fechaEntrada, DateTime fechaSalida);
}

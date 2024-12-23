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
        DateOnly fechaEntrada, DateOnly fechaSalida);
    Task<Reservacion> eliminar(int idReservacion);
    Task<Reservacion> obtenerPorId(int idReservacion);
    
}

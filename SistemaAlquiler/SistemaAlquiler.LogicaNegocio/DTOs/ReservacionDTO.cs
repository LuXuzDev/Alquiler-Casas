using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class ReservacionDTO
{
    public int idReservacion { get; set; }
    public int idUsuario { get; set; }
    public int idCasa { get; set; }
    public int cantPersonas { get; set; }
    public DateOnly fechaEntrada { get; set; }
    public DateOnly fechaSalida { get; set; }
    public double costoTotal { get; set; }
}

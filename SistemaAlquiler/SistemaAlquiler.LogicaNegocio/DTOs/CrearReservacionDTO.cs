using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class CrearReservacionDTO
{
    public int idUsuario { get; set; }
    public int idCasa { get; set; }
    public int cantPersonas { get; set; }
    public DateOnly fechaEntrada { get; set; }
    public DateOnly fechaSalida { get; set; }

}

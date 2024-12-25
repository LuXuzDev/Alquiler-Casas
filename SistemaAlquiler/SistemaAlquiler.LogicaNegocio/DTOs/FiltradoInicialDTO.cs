using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class FiltradoInicialDTO
{
    public int? cantMaxPersonas { get; set; }
    public string? nombreCiudad { get; set; }
    public DateOnly? fechaEntrada { get; set; }
    public DateOnly? fechaSalida { get; set; }
}

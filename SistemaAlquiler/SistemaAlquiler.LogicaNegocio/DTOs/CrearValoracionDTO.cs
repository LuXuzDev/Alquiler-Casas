using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class CrearValoracionDTO
{
    public int idUsuario { get; set; }
    public string nombreUsuario { get; set; }
    public int idCasa { get; set; }
    public double puntuacion { get; set; }
    public string comentario { get; set; }
}

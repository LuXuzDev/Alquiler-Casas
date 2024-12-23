using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class ValoracionDTO
{
    public int idValoracion { get; set; }
    public int idCasa { get; set; }
    public double puntuacion { get; set; }
    public string comentario { get; set; }

    //Usuario
    public int idUsuario { get; set; }
    public string nombreUsuario { get; set; }

}

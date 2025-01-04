using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class FotoDTO
{
    public int idFoto { get; set; }
    public int idCasa { get; set; }
    public string direccionURL { get; set; }
    public string direccionName { get; set; }
}

using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class EditarCasaDTO
{
    public int idCasa { get; set; }
    public int idCiudad { get; set; }
    public int idCaracteristica { get; set; }
    public string ciudad { get; set; }
    public string descripcion { get; set; }
    public double precioNoche { get; set; }
    public double precioMes { get; set; }
    public double areaTotal { get; set; }

    public Caracteristicas caracteristicas { get; set; }
}

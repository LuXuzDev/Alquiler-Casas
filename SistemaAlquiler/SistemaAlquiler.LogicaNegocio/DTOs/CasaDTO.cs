using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SistemaAlquiler.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class CasaDTO
{
    public int idCasa { get; set; }
    public int idCiudad { get; set; }
    public int idCaracteristica { get; set; }
    public string ciudad {  get; set; }
    public string descripcion { get; set; }
    public double precioNoche { get; set; }
    public double precioMes { get; set; }
    public double areaTotal { get; set; }

    public Caracteristicas caracteristicas { get; set; }

    //Usuario
    public int idUsuario { get; set; }
    public string correo { get; set; }
    public string numeroContacto { get; set; }

}

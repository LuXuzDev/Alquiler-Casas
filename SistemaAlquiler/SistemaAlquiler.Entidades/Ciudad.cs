using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.Entidades;

public class Ciudad
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idCiudad {  get; set; }


    [MaxLength (30)]
    public string ciudad { get; set; } = "";

    public Ciudad(string ciudad)
    {
        this.ciudad = ciudad;
    }

}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades;

public class CasaPendiente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idCasa { get; set; }
    public int idCaracteristica { get; set; }
    public int? idUsuario { get; set; }
    public int? idCiudad { get; set; }


    [ForeignKey("idUsuario")]
    public virtual Usuario usuario { get; set; }

    [ForeignKey("idCaracteristica")]
    public virtual Caracteristicas caracteristicas { get; set; }

    [ForeignKey("idCiudad")]
    public virtual Ciudad ciudad { get; set; }

    public virtual ICollection<Foto> fotos { get; set; } = new List<Foto>();

    [Range(0, double.MaxValue)]
    public double precioNoche { get; set; }

    [Range(0, double.MaxValue)]
    public double precioMes { get; set; }

    [Range(0, double.MaxValue)]
    public double areaTotal { get; set; }
    public string descripcion { get; set; }

    public CasaPendiente() { }
    public CasaPendiente(Casa casa)
    {
        idCaracteristica = casa.idCaracteristica;
        idUsuario = casa.idUsuario;
        idCiudad = casa.idCiudad;
        precioNoche = casa.precioNoche;
        precioMes = casa.precioMes;
        areaTotal = casa.areaTotal;
        descripcion = casa.descripcion;
    }
}

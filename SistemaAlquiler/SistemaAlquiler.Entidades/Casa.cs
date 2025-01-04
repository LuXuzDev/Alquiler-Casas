using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades;

public class Casa
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idCasa {  get; set; }
    public int idCaracteristica {  get; set; }
    public int? idUsuario {  get; set; }
    public int? idCiudad { get; set; }

    [ForeignKey("idUsuario")]
    public virtual Usuario usuario { get; set; }

    [ForeignKey("idCaracteristica")]
    public virtual Caracteristicas caracteristicas { get; set; }

    [ForeignKey("idCiudad")]
    public virtual Ciudad ciudad { get; set; }

    public virtual ICollection<Foto> fotos { get; set; } = new List<Foto>();

    [Range(0, double.MaxValue)]
    public double precioNoche {  get; set; }

    [Range(0, double.MaxValue)]
    public double precioMes { get; set; }

    [Range(0, double.MaxValue)]
    public double areaTotal {  get; set; }
    public string descripcion {  get; set; }
    public string nombre { get; set; }
    public string direccion { get; set; }
    public string estado { get; set; }

    public Casa() { }
    public Casa(double precioNoche, double precioMes, double areaTotal, string descripcion, string nombre,string direccion, string estado)
    {
        this.precioNoche = precioNoche;
        this.precioMes = precioMes;
        this.areaTotal = areaTotal;
        this.descripcion = descripcion;
        this.nombre = nombre;
        this.direccion = direccion;
        this.estado = estado;
    }
}

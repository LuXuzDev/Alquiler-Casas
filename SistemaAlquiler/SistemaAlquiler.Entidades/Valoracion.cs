using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades;

public class Valoracion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idValoracion {  get; set; }
    public int idUsuario {  get; set; }
    public int idCasa {  get; set; }
    public double puntuacion { get; set; }
    public string comentario {  get; set; }

    [ForeignKey("idUsuario")]
    public virtual Usuario usuario {  get; set; }
    [ForeignKey("idCasa")]
    public virtual Casa casa { get; set; }

    public Valoracion(int idUsuario, int idCasa, double puntuacion, string comentario)
    {
        this.idUsuario = idUsuario;
        this.idCasa = idCasa;
        this.puntuacion = puntuacion;
        this.comentario = comentario;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idUsuario {  get; set; }
    public int idRol { get; set; }
    public string nombreUsuario { get; set; }
    public string correo {  get; set; }
    public string numeroContacto {  get; set; }
    public string clave { get; set; }

    [ForeignKey("idRol")]
    public virtual Rol rol { get; set; }
    public Usuario() { }
    public Usuario(int idRol,string nombreUsuario ,string correo, string numeroContacto, string clave)
    {
        this.idRol = idRol;
        this.nombreUsuario = nombreUsuario;
        this.correo = correo;
        this.numeroContacto = numeroContacto;
        this.clave = clave;
    }
    
}

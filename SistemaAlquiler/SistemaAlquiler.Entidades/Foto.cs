using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades;

public class Foto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idFoto {  get; set; }
    public int idCasa {  get; set; }

    [ForeignKey("idCasa")]
    public virtual Casa casa { get; set; }
    public string direccionURL { get; set; }
    public string direccionName { get; set; }
    public Foto(int idCasa,string direccionURL, string direccionName)
    {
        this.idCasa = idCasa;
        this.direccionURL = direccionURL;
        this.direccionName = direccionName;
    }
}

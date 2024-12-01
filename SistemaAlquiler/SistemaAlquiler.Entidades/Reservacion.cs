using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades;

public class Reservacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int idReservacion { get; set; }
    public int idUsuario { get; set; }

    [ForeignKey("idUsuario")]
    public virtual Usuario usuario { get; set; }
    public int idCasa {  get; set; }

    [ForeignKey("idCasa")]
    public virtual Casa casa { get; set; }

    public int cantPersonas {  get; set; }
    public DateTime fechaEntrada { get; set; }
    public DateTime fechaSalida { get;set; }
    public double costoTotal {  get; set; }

    public Reservacion() { }
    public Reservacion( int idUsuario, int idCasa,int cantPersonas, 
        DateTime fechaEntrada, DateTime fechaSalida,double costoTotal)
    {
        this.idUsuario = idUsuario;
        this.idCasa = idCasa;
        this.cantPersonas = cantPersonas;
        this.fechaEntrada = fechaEntrada;
        this.fechaSalida = fechaSalida;
        this.costoTotal = costoTotal;
    }
}

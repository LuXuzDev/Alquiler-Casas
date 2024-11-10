using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades
{
    public class Casa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idCasa {  get; set; }

        public int idUsuario {  get; set; }

        [ForeignKey("idUsuario")]
        public virtual Usuario usuario { get; set; }

        public double precioNoche {  get; set; }
        public string direccion { get; set; }
        public double areaTotal {  get; set; }
        public string descripcion {  get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int idUsuario {  get; set; }
        public string correo {  get; set; }
        public string numeroContacto {  get; set; }
        public  string clave { get; set; }
        public string rol {  get; set; }
    }
}

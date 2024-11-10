using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades
{
    public class Caracteristicas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int idCaracteristicas {  get; set; }
        public  int idCasa { get; set; }
        public  int cantMaxPersonas {  get; set; }
        [ForeignKey("idCasa")]
        public virtual Casa casa { get; set; }
        /**************************************************************************************/
        //Estructura
        public int cantHabitaciones {  get; set; }
        public int cantBanos {  get; set; }
        public int cantCuartos {  get; set; }
        public Boolean cocina { get; set; }
        public Boolean terraza_balcon {  get; set; }
        public Boolean barbacoa {  get; set; }
        public Boolean garaje {  get; set; }
        public Boolean piscina {  get; set; }
        /*************************************************************************************/
        //Electrodomesticos
        public Boolean gimnasio {  get; set; }
        public Boolean lavadora_secadora {  get; set; }
        public Boolean tv {  get; set; }
        /*************************************************************************************/
        //Normas
        public Boolean permiteMenores {  get; set; }
        public Boolean permiteFumar {  get; set; }
        public Boolean permiteMascotas {get; set; }
        /*************************************************************************************/
        //Extras
        public Boolean wifi {  get; set; }
        public Boolean aguaCaliente {  get; set; }
        public Boolean climatizada {  get; set; }
    }
}

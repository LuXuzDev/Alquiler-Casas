using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAlquiler.Entidades;

public class Caracteristicas
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public  int idCaracteristicas {  get; set; }
    public  int cantMaxPersonas {  get; set; }
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

    public Caracteristicas (int cantMaxPersonas, int cantHabitaciones, int cantBanos,
        int cantCuartos, bool cocina, bool terraza_balcon,
        bool barbacoa, bool garaje, bool piscina, bool gimnasio, bool lavadora_secadora, bool tv,
        bool permiteMenores, bool permiteFumar, bool permiteMascotas, bool wifi,
        bool aguaCaliente, bool climatizada)
    {
        this.cantMaxPersonas = cantMaxPersonas;
        this.cantHabitaciones = cantHabitaciones;
        this.cantBanos = cantBanos;
        this.cantCuartos = cantCuartos;
        this.cocina = cocina;
        this.terraza_balcon = terraza_balcon;
        this.barbacoa = barbacoa;
        this.garaje = garaje;
        this.piscina = piscina;
        this.gimnasio   = gimnasio;
        this.lavadora_secadora  = lavadora_secadora;
        this.tv = tv;
        this.aguaCaliente = aguaCaliente;
        this.climatizada = climatizada;
    }
}

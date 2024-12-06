using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class CrearCaracteristicasDTO
{
    public int cantMaxPersonas { get; set; }

    //Estructura
    public int cantHabitaciones { get; set; }
    public int cantBanos { get; set; }
    public int cantCuartos { get; set; }
    public Boolean cocina { get; set; }
    public Boolean terraza_balcon { get; set; }
    public Boolean barbacoa { get; set; }
    public Boolean garaje { get; set; }
    public Boolean piscina { get; set; }

    //Electrodomesticos
    public Boolean gimnasio { get; set; }
    public Boolean lavadora_secadora { get; set; }
    public Boolean tv { get; set; }

    //Normas
    public Boolean permiteMenores { get; set; }
    public Boolean permiteFumar { get; set; }
    public Boolean permiteMascotas { get; set; }

    //Extras
    public Boolean wifi { get; set; }
    public Boolean aguaCaliente { get; set; }
    public Boolean climatizada { get; set; }
}

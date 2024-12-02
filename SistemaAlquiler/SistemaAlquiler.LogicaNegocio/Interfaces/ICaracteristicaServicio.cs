using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ICaracteristicaServicio
{
    Task<Caracteristicas> crear(int cantMaxPersonas, int cantHabitaciones, int cantBanos,
        int cantCuartos, Boolean cocina, Boolean terraza_balcon, Boolean barbacoa, Boolean garaje,
        Boolean piscina, Boolean gimnasio, Boolean lavadora_secadora, Boolean tv, Boolean permiteMenores,
        Boolean permiteFumar, Boolean permiteMascotas, Boolean wifi, Boolean aguaCaliente, Boolean climatizada);
    Task<Caracteristicas> editar(int idCaracteristica, int? cantMaxPersonas, int? cantHabitaciones, int? cantBanos,
        int? cantCuartos, Boolean? cocina, Boolean? terraza_balcon, Boolean? barbacoa, Boolean? garaje,
        Boolean? piscina, Boolean? gimnasio, Boolean? lavadora_secadora, Boolean? tv, Boolean? permiteMenores,
        Boolean? permiteFumar, Boolean? permiteMascotas, Boolean? wifi, Boolean? aguaCaliente, Boolean? climatizada);
    Task<Caracteristicas> eliminar(int idCaracteristica);
    Task<Caracteristicas> obtenerPorId(int idCaracteristica);
}

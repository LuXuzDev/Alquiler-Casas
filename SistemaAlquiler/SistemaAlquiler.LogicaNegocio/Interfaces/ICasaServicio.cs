using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ICasaServicio
{
    Task<List<Casa>> lista();
    Task<Casa> crear(int idCaracteristicas, double precioNoche, double precioMes, double areaTotal,
        string descripcion, int idCiudad, int idGestor);
    Task<Casa> editar(Casa casa);
    Task<Casa> eliminar(int idCasa);
    Task<Casa> obtenerPorId(int idCasa);
    Task<List<Casa>> obtenerCasasFiltradas(int? cantMaxPersonas, int? cantHabitaciones, int? cantBanos,
        int? cantCuartos, Boolean? cocina, Boolean? terraza_balcon, Boolean? barbacoa, Boolean? garaje,
        Boolean? piscina, Boolean? gimnasio, Boolean? lavadora_secadora, Boolean? tv, Boolean? permiteMenores,
        Boolean? permiteFumar, Boolean? permiteMascotas, Boolean? wifi, Boolean? aguaCaliente, Boolean? climatizada,
        double? precioNoche,double? precioMes,double? areaTotal 
        );


    Task<List<Casa>> obtenerCasaPorCiudad(int idCiudad);
}

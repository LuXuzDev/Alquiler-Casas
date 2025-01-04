using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface IGestorServicio
{
    Task<List<Casa>> listaCasas(int idGestor);
    Task<List<Casa>> casasPendientes(int idGestor);
    Task<double> ganaciasMensuales(int idGestor);
    Task<double> gananciasTotales(int idGestor);
    Task<List<Valoracion>> valoracionesCasa(int idCasa);


}

using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface IValoracionServicio
{
    Task<List<Valoracion>> lista();
    Task<Valoracion> crear(CrearValoracionDTO valoracionDTO);
    Task<Valoracion> editar(ValoracionDTO valoracionDTO);
    Task<Valoracion> eliminar(int idValoracion);
    Task<Valoracion> obtenerPorId(int idValoracion);
    Task<List<Valoracion>> obtenerPorIdCasa(int idCasa);

}

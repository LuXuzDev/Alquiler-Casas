using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ICaracteristicaServicio
{
    Task<Caracteristicas> crear(CrearCaracteristicasDTO caracteristicaDTO);
    Task<Caracteristicas> editar(int idCaracteristica, CaracteristicaDTO caracteristicaDTO);
    Task<Caracteristicas> eliminar(int idCaracteristica);
    Task<Caracteristicas> obtenerPorId(int idCaracteristica);
}

using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ICiudadServicio
{
    Task<List<Ciudad>> lista();
    Task<Ciudad> crear(string nombreCiudad);
    Task<Ciudad> editar(string nombreCiudadNuevo, int idCiudad);
    Task<Ciudad> eliminar(int idCiudad);
    Task<Ciudad> obtenerPorId(int idCiudad);
}



using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface IUsuarioServicio
{
    Task<List<Usuario>> lista();
    Task<Usuario> crear(CrearUsuarioDTO usuarioDTO );
    Task<Usuario> editar(string correo, string numeroContacto, string clave, int idUsuario);
    Task<Usuario> editarRol(int idUsuario,int idRol);
    Task<Usuario> eliminar(int usuarioId);
    Task<Usuario> obtenerPorCredenciales(string correo, string clave);
    Task<Usuario> obtenerPorId(int usuarioId);
    Task<List<Usuario>> obtenerPorRol(int idRol);
}

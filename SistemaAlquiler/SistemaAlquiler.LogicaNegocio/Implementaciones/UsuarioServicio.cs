using SistemaAlquiler.AccesoDatos;
using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class UsuarioServicio : IUsuarioServicio
{

    private readonly IRepositorioGenerico<Usuario> repositorio;
    private readonly IRepositorioGenerico<Casa> repositorioCasa;
    private readonly IUtilidadesServicio utilidadesServicio;

    public UsuarioServicio(IRepositorioGenerico<Usuario> repositorio, IUtilidadesServicio utilidadesServicio, IRepositorioGenerico<Casa> repositorioCasa)
    {
        this.repositorio = repositorio;
        this.repositorioCasa = repositorioCasa;
        this.utilidadesServicio = utilidadesServicio;
    }

    public async Task<Usuario> obtenerPorId(int usuarioId)
    {
        IQueryable<Usuario> consulta = await repositorio.obtener(u=> u.idUsuario == usuarioId);
        Usuario usuario = consulta.FirstOrDefault();
        if (usuario == null)
            throw new TaskCanceledException("El usuario no existe");
        return usuario;
    }

    public async Task<Usuario> obtenerPorCredenciales(string correo, string clave)
    {
        string claveEncriptada = utilidadesServicio.convertirSha256(clave);
        var usuarioEncontrado = await repositorio.obtener(u=> u.correo.Equals(correo) && u.clave.Equals(claveEncriptada));
        return usuarioEncontrado.FirstOrDefault();
    }
    public async Task<List<Usuario>> lista()
    {
        IQueryable<Usuario> consulta = await repositorio.obtener();
        return (List<Usuario>)consulta.ToList();
    }

    public async Task<Usuario> crear(CrearUsuarioDTO user)
    {
        var usuarioExiste = await repositorio.obtener(u=> u.correo==user.correo);
        if (usuarioExiste.FirstOrDefault() != null)
            throw new TaskCanceledException("El correo ya existe");

        try
        {
            string clave = utilidadesServicio.convertirSha256(user.clave);
            Usuario usuarioCreado = new Usuario(user.idRol, user.correo,user.numeroContacto,clave);
            Usuario usuario = await repositorio.crear(usuarioCreado);

            if(usuarioCreado.idUsuario == 0)
                throw new TaskCanceledException("Error en la creacion del usuario");
            return usuarioCreado;
        }
        catch (Exception) 
        { 
            throw;
        }

    }

    public async Task<Usuario> editar(string correo, string numeroContacto, string clave,int idUsuario)
    {
        var usuarioEncontrado = await repositorio.obtener(u => u.idUsuario == idUsuario);

        if (usuarioEncontrado == null)
            throw new TaskCanceledException("El usuario no existe");

        try
        {
            if(correo!=null)
                usuarioEncontrado.FirstOrDefault().correo= correo;
            if (numeroContacto != null)
                usuarioEncontrado.FirstOrDefault().numeroContacto= numeroContacto;
            if (clave != null)
                usuarioEncontrado.FirstOrDefault().clave = utilidadesServicio.convertirSha256(clave);
            bool respuesta = await repositorio.editar(usuarioEncontrado.FirstOrDefault());

            if (!respuesta)
                throw new TaskCanceledException("Error actualizando los datos");
            else
                return usuarioEncontrado.FirstOrDefault();

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Usuario> editarRol(int idUsuario, int idRol )
    {
        var usuarioEncontrado = await repositorio.obtener(u => u.idUsuario == idUsuario);

        if (usuarioEncontrado == null)
            throw new TaskCanceledException("El usuario no existe");

        try
        {
            usuarioEncontrado.FirstOrDefault().idRol = idRol;
            
            bool respuesta = await repositorio.editar(usuarioEncontrado.FirstOrDefault());

            if (!respuesta)
                throw new TaskCanceledException("Error actualizando los datos");
            else
                return usuarioEncontrado.FirstOrDefault();

        }
        catch (Exception)
        {
            throw;
        }
    }

   
    public async Task<Usuario> eliminar(int usuarioId)
    {
        try
        {
            var usuarioEncontrado = await repositorio.obtener(u => u.idUsuario == usuarioId);

            if (usuarioEncontrado == null)
                throw new TaskCanceledException("El usuario no existe");

            var casas = await repositorioCasa.obtener(u => u.idUsuario == usuarioId);


            //Eliminar la referencia del usuario en la casas
            foreach (var casa in casas)
            {
                casa.idUsuario = null;
            }

            bool respuesta = await repositorio.eliminar(usuarioEncontrado.FirstOrDefault());
            if (!respuesta)
                throw new TaskCanceledException("Error al eliminar el usuario");
            return usuarioEncontrado.FirstOrDefault();
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<List<Usuario>> obtenerPorRol(int idRol)
    {
        IQueryable<Usuario> consulta = await repositorio.obtener(u => u.idRol == idRol);
        List<Usuario> gestores = consulta.ToList();
        return gestores;
    }
}

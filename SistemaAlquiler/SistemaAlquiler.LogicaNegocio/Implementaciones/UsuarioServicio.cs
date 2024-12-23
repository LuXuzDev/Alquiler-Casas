using SistemaAlquiler.AccesoDatos;
using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections;
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
    private readonly IValidadorServicio validadorServicio;

    public UsuarioServicio(IRepositorioGenerico<Usuario> repositorio, IUtilidadesServicio utilidadesServicio,
        IRepositorioGenerico<Casa> repositorioCasa, IValidadorServicio validadorServicio)
    {
        this.repositorio = repositorio;
        this.repositorioCasa = repositorioCasa;
        this.utilidadesServicio = utilidadesServicio;
        this.validadorServicio = validadorServicio;
    }

    public async Task<Usuario> obtenerPorId(int usuarioId)
    {

        Usuario usuario =await validadorServicio.existeUsuario(usuarioId, "El usuario no existe");
        return usuario;
    }

    public async Task<Usuario> obtenerPorCredenciales(string nombreUsuario,string correo, string clave)
    {
        string claveEncriptada = utilidadesServicio.convertirSha256(clave);
        var usuarioEncontrado = await repositorio.obtener(u=> u.correo.Equals(correo) && u.clave.Equals(claveEncriptada) && u.nombreUsuario.Equals(nombreUsuario));
        return usuarioEncontrado.FirstOrDefault();
    }
    public async Task<List<Usuario>> lista()
    {
        IQueryable<Usuario> consulta = await repositorio.obtener();
        return (List<Usuario>)consulta.ToList();
    }

    public async Task<Usuario> crear(CrearUsuarioDTO user)
    {

        await validadorServicio.validarNumeroContacto(user.numeroContacto, "El numero de contacto es incorrecto");
        await validadorServicio.validarCorreo(user.correo,"El correo es incorrecto");
        await validadorServicio.validarClave(user.clave, "La clave debe tener mas de 8 caracteres");
        await validadorServicio.validarTextoVacio(user.nombreUsuario, "El nombre de usuario no puede estar vacio");

        try
        {
            string clave = utilidadesServicio.convertirSha256(user.clave);
            Usuario usuarioCreado = new Usuario(3, user.nombreUsuario,user.correo,user.numeroContacto,clave);
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

    public async Task<Usuario> editar(string nombreUsuario,string correo, string numeroContacto, string clave,int idUsuario)
    {
        Usuario usuarioEncontrado = await validadorServicio.existeUsuario(idUsuario, "El usuario no existe");
        await validadorServicio.validarNumeroContacto(numeroContacto, "El numero de contacto es incorrecto");
        await validadorServicio.validarCorreo(correo, "El correo es incorrecto");
        await validadorServicio.validarClave(clave, "La clave debe tener mas de 8 caracteres");
        await validadorServicio.validarTextoVacio(nombreUsuario, "El nombre de usuario no puede estar vacio");

        

        try
        {
            if(nombreUsuario != null)
                usuarioEncontrado.nombreUsuario = nombreUsuario;
            if (correo!=null)
                usuarioEncontrado.correo= correo;
            if (numeroContacto != null)
                usuarioEncontrado.numeroContacto= numeroContacto;
            if (clave != null)
                usuarioEncontrado.clave = utilidadesServicio.convertirSha256(clave);
            bool respuesta = await repositorio.editar(usuarioEncontrado);

            if (!respuesta)
                throw new TaskCanceledException("Error actualizando los datos");
            else
                return usuarioEncontrado;

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Usuario> editarRol(int idUsuario, int idRol )
    {
        Usuario usuarioEncontrado = await validadorServicio.existeUsuario(idUsuario, "El usuario no existe");
        await validadorServicio.existeRol(idRol, "El rol es incorrecto");

        try
        {
            usuarioEncontrado.idRol = idRol;
            
            bool respuesta = await repositorio.editar(usuarioEncontrado);

            if (!respuesta)
                throw new TaskCanceledException("Error actualizando los datos");
            else
                return usuarioEncontrado;

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
            Usuario usuarioEncontrado = await validadorServicio.existeUsuario(usuarioId, "El usuario no existe");

            if (usuarioEncontrado == null)
                throw new TaskCanceledException("El usuario no existe");

            var casas = await repositorioCasa.obtener(u => u.idUsuario == usuarioId);


            //Eliminar la referencia del usuario en la casas
            foreach (var casa in casas)
            {
                casa.idUsuario = null;
            }

            bool respuesta = await repositorio.eliminar(usuarioEncontrado);
            if (!respuesta)
                throw new TaskCanceledException("Error al eliminar el usuario");
            return usuarioEncontrado;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<List<Usuario>> obtenerPorRol(int idRol)
    {
        await validadorServicio.existeRol(idRol, "El rol es incorrecto");
        IQueryable<Usuario> consulta = await repositorio.obtener(u => u.idRol == idRol);
        List<Usuario> gestores = consulta.ToList();
        return gestores;
    }
}

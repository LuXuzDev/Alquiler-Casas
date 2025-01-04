using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class ValidadorServicio : IValidadorServicio
{
    private readonly IRepositorioGenerico<Casa> casas;
    private readonly IRepositorioGenerico<Usuario> usuarios;
    private readonly IRepositorioGenerico<Rol> roles;
    private readonly IRepositorioGenerico<Ciudad> ciudades;
    private readonly IRepositorioGenerico<Reservacion> reservaciones;
    private readonly IRepositorioGenerico<Valoracion> valoraciones;
    private readonly IRepositorioGenerico<Caracteristicas> caracteristicas;

    public ValidadorServicio(IRepositorioGenerico<Casa> casas,IRepositorioGenerico<Usuario> usuarios,
        IRepositorioGenerico<Rol> roles,IRepositorioGenerico<Ciudad> ciudades,
        IRepositorioGenerico<Reservacion> reservaciones,IRepositorioGenerico<Valoracion> valoraciones,
        IRepositorioGenerico<Caracteristicas> caracteristicas)
    {
        this.casas = casas;
        this.usuarios = usuarios;
        this.roles = roles;
        this.ciudades = ciudades;
        this.reservaciones = reservaciones;
        this.valoraciones = valoraciones;
        this.caracteristicas = caracteristicas;
    }

    public async Task<Casa> existeCasa(int idCasa, string mensaje)
    {
        var consulta = await casas.obtener(u => u.idCasa == idCasa, [u => u.caracteristicas, u => u.ciudad, u=>u.usuario]);
        Casa casa = consulta.FirstOrDefault();
        if(casa == null)
            throw new TaskCanceledException(mensaje);
        return casa;
    }

    public async Task<Ciudad> existeCuidad(int idCiudad, string mensaje)
    {
        var consulta = await ciudades.obtener(u => u.idCiudad == idCiudad);
        Ciudad ciudad = consulta.FirstOrDefault();
        if (ciudad == null)
            throw new TaskCanceledException(mensaje);
        return ciudad;
    }

    public async Task<Usuario> existeGestor(int idGestor, string mensaje)
    {
        var consulta = await usuarios.obtener(u => u.idUsuario == idGestor && u.idRol==2);
        Usuario usuario = consulta.FirstOrDefault();
        if (usuario == null)
            throw new TaskCanceledException(mensaje);
        return usuario;
    }

    public async Task<Reservacion> existeReservacion(int idReservacion, string mensaje)
    {
        var consulta = await reservaciones.obtener(u => u.idReservacion == idReservacion);
        Reservacion reservacion = consulta.FirstOrDefault();
        if (reservacion == null)
            throw new TaskCanceledException(mensaje);
        return reservacion;
    }

    public async Task<Rol> existeRol(int idRol, string mensaje)
    {
        var consulta = await roles.obtener(u => u.idRol == idRol);
        Rol rol = consulta.FirstOrDefault();
        if (rol == null)
            throw new TaskCanceledException(mensaje);
        return rol;
    }

    public async Task<Usuario> existeUsuario(int idUsuario, string mensaje)
    {
        var consulta = await usuarios.obtener(u => u.idUsuario == idUsuario);
        Usuario usuario = consulta.FirstOrDefault();
        if (usuario == null)
            throw new TaskCanceledException(mensaje);
        return usuario;
    }

    public async Task<Valoracion> existeValoracion(int idValoracion, string mensaje)
    {
        var consulta = await valoraciones.obtener(u => u.idValoracion == idValoracion);
        Valoracion valoracion = consulta.FirstOrDefault();
        if (valoracion == null)
            throw new TaskCanceledException(mensaje);
        return valoracion;
    }

    public async Task<Caracteristicas> existeCaracteristica(int idCaracteristica, string mensaje)
    {
        var consulta =await caracteristicas.obtener(u => u.idCaracteristicas == idCaracteristica);
        Caracteristicas caracteristica = consulta.FirstOrDefault();
        if (caracteristica == null)
            throw new TaskCanceledException(mensaje);
        return caracteristica;
    }

    public async Task<string> validarCiudad(string nombreCiudad, string mensaje)
    {
        var consulta = await ciudades.obtener(u => u.ciudad==nombreCiudad);
        Ciudad valoracion = consulta.FirstOrDefault();
        if (valoracion != null)
            throw new TaskCanceledException(mensaje);
        return nombreCiudad;
    }

    public async Task<string> validarCorreo(string correo, string mensaje)
    {
        await validarTextoVacio(correo, mensaje);
        var usuarioExiste = await usuarios.obtener(u => u.correo == correo);
        if (usuarioExiste.FirstOrDefault() != null)
            throw new TaskCanceledException("El correo ya existe");
        else
        {
            //Validacion del correo
        }
        return correo;
    }

    public async Task<string> validarNumeroContacto(string numeroContacto, string mensaje)
    {
        bool esCorrecta = true;

        char[] temp = numeroContacto.ToCharArray();
        foreach (char c in temp)
        {
            if (Char.IsLetter(c))
                esCorrecta = false;
        }

        if (!esCorrecta)
            throw new TaskCanceledException(mensaje);
        return numeroContacto;
    }

    public async Task<double> validarNumerosDouble(double limInferior, double limSuperior,double num, string mensaje)
    {
        if(num<limInferior || num> limSuperior)
            throw new TaskCanceledException(mensaje);
        return num;
    }

    public async Task<int> validarNumerosEnteros(int limInferior, int limSuperior,int num, string mensaje)
    {
        if (num < limInferior || num > limSuperior)
            throw new TaskCanceledException(mensaje);
        return num;
    }

    public async Task<string> validarTextoVacio(string texto, string mensaje)
    {
        if (texto.IsEmpty()) 
            throw new TaskCanceledException(mensaje);
        return texto;
    }

    public async Task<string> validarClave(string clave, string mensaje)
    {
        await validarTextoVacio(clave, mensaje);
        if(clave.Length<8)
            throw new TaskCanceledException(mensaje);
        return clave;
    }
}

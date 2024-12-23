using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface IValidadorServicio
{
    Task<Usuario> existeUsuario(int idUsuario,string mensaje);
    Task<Casa> existeCasa(int idCasa, string mensaje);
    Task<CasaPendiente> existeCasaPendiente(int idCasaPendiente, string mensaje);
    Task<Usuario> existeGestor(int idGestor, string mensaje);
    Task<Reservacion> existeReservacion(int idReservacion, string mensaje);
    Task<Ciudad> existeCuidad(int idCiudad, string mensaje);
    Task<Valoracion> existeValoracion(int idValoracion, string mensaje);
    Task<Rol> existeRol(int idRol, string mensaje);
    Task<Caracteristicas> existeCaracteristica(int idCaracteristicas, string mensaje);
    Task<double> validarNumerosDouble(double limInferior, double limSuperior,double num, string mensaje);
    Task<int> validarNumerosEnteros(int limInferior, int limSuperior, int num, string mensaje);
    Task<string> validarNumeroContacto(string numeroContacto, string mensaje);
    Task<string> validarCorreo(string correo, string mensaje);
    Task<string> validarTextoVacio(string texto, string mensaje);
    Task<string> validarCiudad(string nombreCiudad, string mensaje);
    Task<string> validarClave(string clave, string mensaje);

}

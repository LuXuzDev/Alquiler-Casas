using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class LoginServicio : ILoginServicio
{
    private readonly IUsuarioServicio usuarioServicio;
    public async Task<bool> login(string correo, string clave)
    {
        try
        {
            var usuario = await usuarioServicio.obtenerPorCredenciales(correo, clave);
            if (usuario == null)
                throw new TaskCanceledException("El usuario no existe");
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

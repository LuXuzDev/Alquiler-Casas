using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using SistemaAlquiler.LogicaNegocio.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class LoginServicio : ILoginServicio
{
    private readonly IUsuarioServicio usuarioServicio;
    private readonly CreadorToken creadorToken;

    public LoginServicio(IUsuarioServicio usuarioServicio, CreadorToken creadorToken)
    {
        this.usuarioServicio = usuarioServicio;
        this.creadorToken = creadorToken;
    }

    public async Task<LoginRetornoDTO> login(string nombreUsuario,string correo, string clave)
    {
        try
        {
            Usuario usuario = await usuarioServicio.obtenerPorCredenciales(nombreUsuario,correo, clave);
            if (usuario == null)
                throw new TaskCanceledException("Usuario incorrecto");
            
            LoginRetornoDTO ret = new LoginRetornoDTO();
            ret.token= creadorToken.crearToken(usuario);
            ret.idUsuario = usuario.idUsuario;
            return ret;
        }
        catch (Exception)
        {
            throw;
        }
    }
}

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
            
<<<<<<< HEAD
            LoginRetornoDTO retornoDTO = new LoginRetornoDTO();
            retornoDTO.token = creadorToken.crearToken(usuario);
            retornoDTO.idUsuario=usuario.idUsuario;
            return retornoDTO;
=======
            LoginRetornoDTO ret = new LoginRetornoDTO();
            ret.token= creadorToken.crearToken(usuario);
            ret.idUsuario = usuario.idUsuario;
            return ret;
>>>>>>> feace3b7b76e39484f78849713366e2432ea631b
        }
        catch (Exception)
        {
            throw;
        }
    }
}

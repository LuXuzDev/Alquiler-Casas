using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ILoginServicio
{
    Task<LoginRetornoDTO> login(string nombreUsuario,string correo, string clave);
}

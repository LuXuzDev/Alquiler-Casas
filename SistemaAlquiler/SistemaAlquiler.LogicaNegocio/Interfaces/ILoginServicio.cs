using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ILoginServicio
{
    Task<string> login(string nombreUsuario,string correo, string clave);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class LoginDTO
{
    public string nombreUsuario {  get; set; }
    public string correo { get; set; }
    public string clave { get; set; }
}

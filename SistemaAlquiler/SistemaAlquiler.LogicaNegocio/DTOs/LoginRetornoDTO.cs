using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class LoginRetornoDTO
{
    public string token { get; set; }
    public int idUsuario {  get; set; }
}

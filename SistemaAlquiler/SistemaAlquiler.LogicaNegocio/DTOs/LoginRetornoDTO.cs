using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.DTOs;

public class LoginRetornoDTO
{
    public int idUsuario {  get; set; }
    public string token { get; set; }
}

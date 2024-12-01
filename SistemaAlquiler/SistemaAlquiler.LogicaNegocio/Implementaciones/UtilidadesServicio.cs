using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class UtilidadesServicio : IUtilidadesServicio
{
    public string generarClave()
    {
        string clave = Guid.NewGuid().ToString("N").Substring(0,10);
        return clave;
    }
    public string convertirSha256(string clave)
    {
        StringBuilder sb = new StringBuilder();
        using(SHA256 hash = SHA256Managed.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] resultado = hash.ComputeHash(enc.GetBytes(clave));
            foreach(byte b in resultado)
            {
                sb.Append(b.ToString("x2"));
            }
        }
        return sb.ToString();
    }

    
}

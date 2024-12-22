using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Tokens;
using SistemaAlquiler.Entidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.JWT;

public class CreadorToken(IConfiguration configuration)
{

    public string crearToken(Usuario usuario)
    {
        string clave = configuration["Jwt:Key"];

        var claveSecreta = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(clave));
        var credenciales = new SigningCredentials(claveSecreta, SecurityAlgorithms.HmacSha256);
        var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.idUsuario.ToString()),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, usuario.correo), 
        new Claim(ClaimTypes.Role, usuario.idRol.ToString()) 
    };


        var tokenDescripcion = new SecurityTokenDescriptor
        {
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(120),
            SigningCredentials = credenciales
        };


        var handler = new JwtSecurityTokenHandler();
        var token = handler.CreateToken(tokenDescripcion);
        return handler.WriteToken(token);
    }
}

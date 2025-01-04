using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SistemaAlquiler.AccesoDatos;
using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.AccesoDatos.Repositorios;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using SistemaAlquiler.LogicaNegocio.Implementaciones;
using SistemaAlquiler.LogicaNegocio.JWT;


namespace SistemaAlquiler.Controladora;

public static class Dependencia
{
    public static void inyectarDependencia(this IServiceCollection servicios, IConfiguration configuracion)
    {
        servicios.AddDbContext<DB_Context>(opciones =>
        {
            opciones.UseNpgsql(configuracion.GetConnectionString("PostgreSQLConnection"));
            
        });


        servicios.AddTransient(typeof(IRepositorioGenerico<>), typeof(RepositorioGenerico<>));
        servicios.AddScoped<IUtilidadesServicio, UtilidadesServicio>();
        servicios.AddScoped<IUsuarioServicio, UsuarioServicio>();
        servicios.AddScoped<ICasaServicio, CasaServicio>();
        servicios.AddScoped<ICaracteristicaServicio, CaracteristicaServicio>();
        servicios.AddScoped<ICiudadServicio, CiudadServicio>();
        servicios.AddScoped<IReservacionServicio, ReservacionServicio>();
        servicios.AddScoped<ILoginServicio, LoginServicio>();
        servicios.AddScoped<IGestorServicio, GestorServicio > ();
        servicios.AddScoped<IValoracionServicio, ValoracionServicio>();
        servicios.AddScoped<IValidadorServicio, ValidadorServicio>();
        servicios.AddSingleton<CreadorToken>();
        
        
    }
}

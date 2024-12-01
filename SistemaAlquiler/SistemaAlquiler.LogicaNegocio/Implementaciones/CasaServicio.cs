using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class CasaServicio : ICasaServicio
{
    private readonly IRepositorioGenerico<Casa> repositorio;
    
    public CasaServicio(IRepositorioGenerico<Casa> repositorio)
    {
        this.repositorio = repositorio;
    }
    
    public async Task<Casa> crear(int idCaracteristicas, double precioNoche, double precioMes, double areaTotal, 
        string descripcion, int idCiudad,int idGestor)
    {
        Casa casa = new Casa(precioNoche, precioMes,areaTotal,descripcion);
        casa.idCaracteristica = idCaracteristicas;
        casa.idCiudad = idCiudad;
        casa.idUsuario = idGestor;
        var casaConstruida = await repositorio.crear(casa);
        return casaConstruida;
    }

    public Task<Casa> editar(Casa casa)
    {
        throw new NotImplementedException();
    }

    public Task<Casa> eliminar(int idCasa)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Casa>> lista()
    {
        IQueryable<Casa> consulta = await repositorio.obtener(u => u.idUsuario != null && u.idCiudad != null, [u => u.usuario,u=> u.caracteristicas, u=> u.ciudad]);
        List<Casa> casas = consulta.ToList();
        return casas;
    }

    public async Task<List<Casa>> obtenerCasaPorCiudad(int idCiudad)
    {
        IQueryable<Casa> consulta = await repositorio.obtener(u => u.idCiudad == idCiudad && u.idUsuario != null);
        List<Casa> casas = consulta.ToList();
        return casas;
    }


    public async Task<List<Casa>> obtenerCasasFiltradas(int? cantMaxPersonas, int? cantHabitaciones, int? cantBanos, int? cantCuartos,
        bool? cocina, bool? terraza_balcon, bool? barbacoa, bool? garaje, bool? piscina,
        bool? gimnasio, bool? lavadora_secadora, bool? tv, bool? permiteMenores, bool? permiteFumar,
        bool? permiteMascotas, bool? wifi, bool? aguaCaliente, bool? climatizada, 
        double? precioNoche, double? precioMes, double? areaTotal)
    {
        IQueryable<Casa> casas = await repositorio.obtener(u=>u.idUsuario != null && u.idCiudad != null);
        if (cantMaxPersonas.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.cantMaxPersonas <= cantMaxPersonas);

        if (cantHabitaciones.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.cantHabitaciones <= cantHabitaciones);

        if (cantBanos.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.cantBanos <= cantBanos);

        if (cantCuartos.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.cantCuartos <= cantCuartos);

        if (cocina.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.cocina == cocina);

        if (terraza_balcon.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.terraza_balcon == terraza_balcon);

        if (barbacoa.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.barbacoa == barbacoa);

        if (garaje.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.garaje == garaje);

        if (piscina.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.piscina == piscina);

        if (gimnasio.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.gimnasio == gimnasio);

        if (lavadora_secadora.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.lavadora_secadora == lavadora_secadora);

        if (tv.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.tv == tv);

        if (permiteMenores.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.permiteMenores == permiteMenores);

        if (permiteFumar.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.permiteFumar == permiteFumar);

        if (permiteMascotas.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.permiteMascotas == permiteMascotas);

        if (wifi.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.wifi == wifi);

        if (aguaCaliente.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.aguaCaliente == aguaCaliente);

        if (climatizada.HasValue)
            casas = await repositorio.obtener(u => u.caracteristicas.climatizada == climatizada);

        if (precioNoche.HasValue)
            casas = await repositorio.obtener(u => u.precioNoche <= precioNoche);

        if (precioMes.HasValue)
            casas = await repositorio.obtener(u => u.precioMes <= precioMes);

        if (areaTotal.HasValue)
            casas = await repositorio.obtener(u => u.areaTotal <= areaTotal);

        List<Casa> lista = casas.ToList();
        return lista;
    }

    public Task<Casa> obtenerPorId(int idCasa)
    {
        var casa = repositorio.obtenerTodos(u=> u.idCasa==idCasa);
        return casa;
    }
}

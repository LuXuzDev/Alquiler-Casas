using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class GestorServicio : IGestorServicio
{
    private readonly IRepositorioGenerico<Casa> repositorioCasa;
    private readonly IRepositorioGenerico<Usuario> repositorioUsuarios;
    private readonly IRepositorioGenerico<Reservacion> repositorioReservaciones;
    private readonly IValoracionServicio valoracionServicio;
    private readonly IValidadorServicio validadorServicio;

    public GestorServicio(IRepositorioGenerico<Casa> repositorioCasa,IRepositorioGenerico<Reservacion> repositorioReservaciones,
        IRepositorioGenerico<Usuario> repositorioUsuarios,IValoracionServicio valoracionServicio,
        IValidadorServicio validadorServicio)
    {
        this.repositorioCasa = repositorioCasa;
        this.repositorioReservaciones = repositorioReservaciones;
        this.repositorioUsuarios = repositorioUsuarios;
        this.valoracionServicio = valoracionServicio;
        this.validadorServicio = validadorServicio;
    }

    public async Task<List<Casa>> casasPendientes(int idGestor)
    {
        await validadorServicio.existeGestor(idGestor, "No existe ese gestor");
        var consulta = await repositorioCasa.obtener(u=> u.idUsuario==idGestor && u.estado.Equals("Pendiente"), [u => u.caracteristicas, u => u.ciudad, u => u.fotos]);
        List<Casa> casas = consulta.ToList();
        return casas;
    }

    public async Task<List<Casa>> listaCasas(int idGestor)
    {
        await validadorServicio.existeGestor(idGestor, "No existe ese gestor");
        var consulta = await repositorioCasa.obtener(u => u.idUsuario == idGestor && u.estado.Equals("Publicada"), [u => u.caracteristicas, u => u.ciudad, u => u.fotos]);
        List<Casa> casas = consulta.ToList();
        return casas;
    }

    public async Task<List<Reservacion>> listaReservacion(int idGestor)
    {
        return await obtenerPorGestor(idGestor);
    }

    private async Task<List<Reservacion>> obtenerPorGestor(int idGestor)
    {
        await validadorServicio.existeGestor(idGestor, "No existe ese gestor");
        List<Casa> casas = await listaCasas(idGestor);
        List<Reservacion> reservaciones=new List<Reservacion>();
        foreach (Casa c in casas)
        {
            var consulta = await repositorioReservaciones.obtener(u => u.idCasa == c.idCasa);
            List<Reservacion> reservacion = consulta.ToList();
            if (reservacion != null)
                reservaciones.AddRange(reservacion);
        }
        return reservaciones;
    }

    public async Task<double> ganaciasMensuales(int idGestor)
    {
        await validadorServicio.existeGestor(idGestor, "No existe ese gestor");
        List<Reservacion> reservaciones = await obtenerPorGestor(idGestor);
        double total = 0;
        int mes = DateTime.Now.Month;
        foreach (Reservacion r in reservaciones)
        {
            if(r.fechaEntrada.Month== mes)
                total += r.costoTotal;
        }
        return total;
    }

    public async Task<double> gananciasTotales(int idGestor)
    {
        await validadorServicio.existeGestor(idGestor, "No existe ese gestor");
        List<Reservacion> reservaciones = await obtenerPorGestor(idGestor);
        double total = 0;
        foreach (Reservacion r in reservaciones)
        {
            total += r.costoTotal;
        }
        return total;
    }


    public async Task<List<Valoracion>> valoracionesCasa(int idCasa)
    {
        List<Valoracion> valoraciones = await valoracionServicio.obtenerPorIdCasa(idCasa);
        return valoraciones;
    }
}

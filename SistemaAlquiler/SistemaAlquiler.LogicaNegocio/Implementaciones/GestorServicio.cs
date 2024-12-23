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
    private readonly IRepositorioGenerico<CasaPendiente> repositorioPendientes;
    private readonly IRepositorioGenerico<Reservacion> repositorioReservaciones;

    public GestorServicio(IRepositorioGenerico<Casa> repositorioCasa, IRepositorioGenerico<CasaPendiente> repositorioPendientes,
        IRepositorioGenerico<Reservacion> repositorioReservaciones, IRepositorioGenerico<Usuario> repositorioUsuarios)
    {
        this.repositorioCasa = repositorioCasa;
        this.repositorioPendientes = repositorioPendientes;
        this.repositorioReservaciones = repositorioReservaciones;
        this.repositorioUsuarios = repositorioUsuarios;
    }

    public async Task<List<CasaPendiente>> casasPendientes(int idGestor)
    {
        await existeGestor(idGestor);
        var consulta = await repositorioPendientes.obtener(u=> u.idUsuario==idGestor);
        List<CasaPendiente> casas = consulta.ToList();
        return casas;
    }

    public async Task<List<Casa>> listaCasas(int idGestor)
    {
        await existeGestor(idGestor);
        var consulta = await repositorioCasa.obtener(u => u.idUsuario == idGestor);
        List<Casa> casas = consulta.ToList();
        return casas;
    }

    private async Task<List<Reservacion>> obtenerPorGestor(int idGestor)
    {
        await existeGestor(idGestor);
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
        await existeGestor(idGestor);
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
        await existeGestor(idGestor);
        List<Reservacion> reservaciones = await obtenerPorGestor(idGestor);
        double total = 0;
        foreach (Reservacion r in reservaciones)
        {
            total += r.costoTotal;
        }
        return total;
    }

    private async Task<bool> existeGestor(int idGestor)
    {
        bool existe = true;
        var consulta = await repositorioUsuarios.obtener(u=> u.idUsuario==idGestor && u.idRol==2);
        Usuario gestor = consulta.FirstOrDefault();
        if (gestor==null)
            throw new TaskCanceledException("El gestor no existe");
        return existe;
    }

}

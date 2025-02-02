using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class AdministradorServicio : IAdministradorServicio
{
    IReservacionServicio reservacionServicio;

    public AdministradorServicio(IReservacionServicio reservacionServicio)
    {
        this.reservacionServicio = reservacionServicio;
    }
    
    public async Task<double> ganaciasMensuales()
    {
        var consulta = await reservacionServicio.lista();
        List<Reservacion> reservaciones = consulta.ToList();
        double total = 0;
        foreach (Reservacion r in reservaciones)
        {
            if(r.fechaEntrada.Month== DateTime.Now.Month)
                total += r.costoTotal;
        }

        return total * 0.05;
    }

    public async Task<double> ganaciasTotales()
    {
        var consulta = await reservacionServicio.lista();
        List<Reservacion> reservaciones = consulta.ToList();
        double total = 0;
        foreach(Reservacion r in reservaciones)
        {
            total += r.costoTotal;
        }

        return total * 0.05;
    }
}

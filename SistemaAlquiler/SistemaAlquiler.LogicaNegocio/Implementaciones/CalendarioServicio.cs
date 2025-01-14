using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class CalendarioServicio : ICalendarioServicio
{
    private readonly IRepositorioGenerico<Reservacion> repositorio;
    private readonly IValidadorServicio validadorServicio;

    public CalendarioServicio(IRepositorioGenerico<Reservacion> repositorio, IValidadorServicio validadorServicio)
    {
        this.repositorio = repositorio;
        this.validadorServicio = validadorServicio;
    }

    public async Task<List<DateOnly>> fechasOcupadas(int idCasa)
    {
        Casa casa = await validadorServicio.existeCasa(idCasa,"La casa no existe");
        List<DateOnly> result = new List<DateOnly>();
        var consulta = await repositorio.obtener(u=> u.idCasa == idCasa);
        List<Reservacion> reservaciones = consulta.ToList();
        
        foreach(Reservacion r in reservaciones)
        {
            DateOnly entrada = r.fechaEntrada;
            DateOnly salida = r.fechaSalida;

            while (entrada <= salida)
            {
                if(!result.Contains(entrada))
                    result.Add(entrada);
                entrada = entrada.AddDays(1);
            }
        }
        return result;
    }
}

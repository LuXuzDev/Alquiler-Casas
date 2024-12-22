using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ICasaPendienteServicio
{
    Task<List<CasaPendiente>> listaPendientes();
    Task<CasaPendiente> crear(Casa casa, CrearCaracteristicasDTO caracteristicas);
    Task<Casa> publicar(int id);
    Task<CasaPendiente> editar(EditarCasaDTO casa, CaracteristicaDTO caracteristicas);
    Task<CasaPendiente> cancelarSolicitud(int id);
}

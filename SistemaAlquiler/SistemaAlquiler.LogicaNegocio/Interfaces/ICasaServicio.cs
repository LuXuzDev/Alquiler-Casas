using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface ICasaServicio
{
    Task<List<Casa>> lista();
    Task<Casa> crear(Casa casa,CrearCaracteristicasDTO caracteristicas);
    Task<Casa> editar(EditarCasaDTO casa,CaracteristicaDTO caracteristicas);
    Task<Casa> eliminar(int idCasa);
    Task<Casa> obtenerPorId(int idCasa);
    Task<List<Casa>> obtenerCasasFiltradas(BusquedaCasaDTO busquedaCasaDTO);
    Task<List<Casa>> obtenerCasaPorCiudad(int idCiudad);
    Task<List<Casa>> filtradoInicial(FiltradoInicialDTO filtrado);
}

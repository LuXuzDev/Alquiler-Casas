using Microsoft.AspNetCore.Http;
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
    Task<List<Casa>> listaPendientes();
    Task<Casa> crear(CrearCasaDTO casa,List<IFormFile> fotos );
    Task<Casa> editar(EditarCasaDTO casa,CaracteristicaDTO caracteristicas);
    Task<Casa> eliminar(int idCasa);
    Task<Casa> obtenerPorId(int idCasa);
    Task<List<Casa>> obtenerCasasFiltradas(BusquedaCasaDTO busquedaCasaDTO);
    Task<List<Casa>> obtenerCasaPorCiudad(int idCiudad);
    Task<List<Casa>> filtradoInicial(FiltradoInicialDTO filtrado);
    Task<Casa> publicarCasa (int idCasa);
}

using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class ValoracionServicio : IValoracionServicio
{
    private readonly IRepositorioGenerico<Valoracion> repositorioValoracion;
    private readonly IRepositorioGenerico<Usuario> repositorioUsuario;
    private readonly IValidadorServicio validadorServicio;

    public ValoracionServicio(IRepositorioGenerico<Valoracion> repositorioValoracion, IRepositorioGenerico<Usuario> repositorioUsuario,
        IValidadorServicio validadorServicio)
    {
        this.repositorioValoracion = repositorioValoracion;
        this.repositorioUsuario = repositorioUsuario;
        this.validadorServicio = validadorServicio;
    }

    public async Task<Valoracion> crear(CrearValoracionDTO valoracionDTO)
    {
        await validadorServicio.existeUsuario(valoracionDTO.idUsuario, "El usuario no existe");
        await validadorServicio.existeCasa(valoracionDTO.idCasa, "La casa no existe");
        await validadorServicio.validarNumerosDouble(0, 5, valoracionDTO.puntuacion, "La puntuacion es incorrecta");
        await validadorServicio.validarTextoVacio(valoracionDTO.comentario, "El comentario no puede estar vacio");

        Valoracion valoracion = new Valoracion(valoracionDTO.idCasa,valoracionDTO.idCasa,valoracionDTO.puntuacion,valoracionDTO.comentario);
        await repositorioValoracion.crear(valoracion);
        return valoracion;
    }

    public async Task<Valoracion> editar(ValoracionDTO valoracionDTO)
    {
        Valoracion valoracion = await obtenerPorId(valoracionDTO.idValoracion);
        if(valoracionDTO.idCasa!=null)
        {
            await validadorServicio.existeCasa(valoracionDTO.idCasa, "La casa no existe");
            valoracion.idCasa = valoracionDTO.idCasa;
        }
            
        if(valoracionDTO.comentario!=null)
        {
            await validadorServicio.validarTextoVacio(valoracionDTO.comentario, "El comentario no puede estar vacio");
            valoracion.comentario = valoracionDTO.comentario;
        }
            
        if(valoracionDTO.puntuacion!=null)
        {
            await validadorServicio.validarNumerosDouble(0, 5, valoracionDTO.puntuacion, "La puntuacion es incorrecta");
            valoracion.puntuacion = valoracionDTO.puntuacion;
        }
           
        await repositorioValoracion.editar(valoracion);
        return valoracion;
    }

    public async Task<Valoracion> eliminar(int idValoracion)
    {
        Valoracion valoracion =await obtenerPorId(idValoracion);
        await repositorioValoracion.eliminar(valoracion);
        return valoracion;
    }

    public async Task<List<Valoracion>> lista()
    {
        var consulta = await repositorioValoracion.obtener(u=> u.idValoracion!=null,[u => u.usuario]);
        List<Valoracion> valoraciones = consulta.ToList();
        return valoraciones;
    }

    public async Task<Valoracion> obtenerPorId(int idValoracion)
    {
        await validadorServicio.existeValoracion(idValoracion, "La valoracion no existe");
        var consulta = await repositorioValoracion.obtener(u=> u.idValoracion== idValoracion, [u => u.usuario]);
        Valoracion valoracion = consulta.FirstOrDefault();
        return valoracion;
    }

    public async Task<List<Valoracion>> obtenerPorIdCasa(int idCasa)
    {
        await validadorServicio.existeCasa(idCasa, "La casa no existe");
        var consulta = await repositorioValoracion.obtener(u=> u.idCasa==idCasa, [u => u.usuario]);
        List<Valoracion> valoraciones = consulta.ToList();
        return valoraciones;
    }
}

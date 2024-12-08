using SistemaAlquiler.AccesoDatos.Interfaces;
using SistemaAlquiler.AccesoDatos.Repositorios;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaAlquiler.LogicaNegocio.Implementaciones;

public class CiudadServicio : ICiudadServicio
{
    private readonly IRepositorioGenerico<Ciudad> repositorio;
    private readonly IRepositorioGenerico<Casa> repositorioCasa;

    public CiudadServicio(IRepositorioGenerico<Ciudad> repositorio, IRepositorioGenerico<Casa> repositorioCasa)
    {
        this.repositorio = repositorio;
        this.repositorioCasa = repositorioCasa;
    }

    public async Task<Ciudad> crear(string nombreCiudad)
    {
        var ciudadExiste = await repositorio.obtener(u=> u.ciudad.Equals(nombreCiudad));
        if (ciudadExiste.FirstOrDefault() != null)
            throw new TaskCanceledException("La ciudad ya existe");

        try
        {
            Ciudad c = new Ciudad(nombreCiudad);
            Ciudad ciudadCreada = await repositorio.crear(c);
            return ciudadCreada;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Ciudad> editar(string nombreCiudadNuevo, int idCiudad)
    {
        IQueryable<Ciudad> consulta = await repositorio.obtener(u => u.idCiudad == idCiudad);
        Ciudad ciudad = consulta.FirstOrDefault();
        if (ciudad == null)
            throw new TaskCanceledException("La ciudad no existe");

        try
        {
            ciudad.ciudad = nombreCiudadNuevo;
            bool respuesta = await repositorio.editar(ciudad);

            if (!respuesta)
                throw new TaskCanceledException("Error actualizando los datos");
            else
                return ciudad;

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Ciudad> eliminar(int idCiudad)
    {
        try
        {
            IQueryable<Ciudad> consulta = await repositorio.obtener(u => u.idCiudad == idCiudad);
            Ciudad ciudad = consulta.FirstOrDefault();
            if (ciudad == null)
                throw new TaskCanceledException("La ciudad no existe");
            var casas = await repositorioCasa.obtener(u => u.idCiudad == idCiudad);


            //Eliminar la referencia de la ciudad en la casas
            foreach (var casa in casas)
            {
                casa.idCiudad = null;
            }

            bool respuesta = await repositorio.eliminar(ciudad);
            if (!respuesta)
                throw new TaskCanceledException("Error al eliminar la ciudad");
            return ciudad;
        }
        catch(Exception)
        {
            throw;
        }

    }

    public async Task<List<Ciudad>> lista()
    {
        IQueryable<Ciudad> consulta = await repositorio.obtener();
        return (List<Ciudad>)consulta.ToList();
    }

    public async Task<Ciudad> obtenerPorId(int idCiudad)
    {
        IQueryable<Ciudad> consulta = await repositorio.obtener(u => u.idCiudad == idCiudad);
        Ciudad ciudad = consulta.FirstOrDefault();
        if(ciudad == null)
            throw new TaskCanceledException("La ciudad no existe");
        return ciudad;
    }
}

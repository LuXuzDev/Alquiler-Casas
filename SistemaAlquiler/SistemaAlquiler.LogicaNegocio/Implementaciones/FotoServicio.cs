using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

public class FotoServicio:IFotoServicio
{
    private readonly string _rutaBase; // Ruta donde se almacenarán las fotos
    private readonly IRepositorioGenerico<Foto> repositorio; // Repositorio para interactuar con la base de datos

    public FotoServicio(IRepositorioGenerico<Foto> repositorio)
    {
        this.repositorio = repositorio;
        _rutaBase = Path.Combine("wwwroot", "fotos"); // Ruta local
        if (!Directory.Exists(_rutaBase))
        {
            Directory.CreateDirectory(_rutaBase); // Crea el directorio si no existe
        }
    }


    public async Task<List<string>> bajarFotosCasa(int idCasa)
    {
        var fotos = await repositorio.obtener(u=> u.idCasa==idCasa); // Método que obtiene fotos por IdCasa
        return fotos.Select(f => f.direccionURL).ToList(); // Devuelve las URLs de las fotos
    }



    public async Task<Foto> eliminarFoto(int idFoto)
    {
        var foto = await repositorio.obtener(u=> u.idFoto==idFoto); 
        if (foto == null)
            throw new TaskCanceledException("La foto no existe");

        Foto temp = foto.FirstOrDefault();
        // Elimina la foto del sistema de archivos
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos", temp.direccionName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        // Elimina la foto de la base de datos
        await repositorio.eliminar(temp);
        return temp; // Devuelve la foto eliminada
    }


    public async Task<List<Foto>> eliminarFotos(int idCasa)
    {
        var fotos = await repositorio.obtener(u => u.idCasa == idCasa);
        List<Foto> temp =fotos.ToList();

        if (temp.Count == 0)
            throw new TaskCanceledException("La casa no tiene fotos");

        // Elimina las fotos del sistema de archivos
        foreach (Foto f in temp)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos", f.direccionName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            await repositorio.eliminar(f);
        }
        return temp;
    }


    public async Task llenarDTO(CasaDTO casaDTO)
    {
        casaDTO.fotosURL = await bajarFotosCasa(casaDTO.idCasa);
    }


    public async Task llenarDTOs(List<CasaDTO> casaDTOs)
    {
        foreach(var f in casaDTOs)
        {
            f.fotosURL = await bajarFotosCasa(f.idCasa);
        }
    }

    public async Task<List<Foto>> subirFoto(List<IFormFile> fotos, int idCasa)
    {
        var fotosSubidas = new List<Foto>();

        foreach (var foto in fotos)
        {
            if (foto.Length > 0)
            {
                // Genera un nombre único para la foto usando Guid
                var nombreAleatorio = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                var direccionURL = Path.Combine(_rutaBase, nombreAleatorio);

                using (var stream = new FileStream(direccionURL, FileMode.Create))
                {
                    await foto.CopyToAsync(stream); // Guarda la foto en el sistema de archivos
                }

                // Crea un objeto Foto con la información necesaria
                Foto fotoSubida = new Foto(idCasa,direccionURL,nombreAleatorio);
                

                await repositorio.crear(fotoSubida); // Guarda en la base de datos
                fotosSubidas.Add(fotoSubida);
            }
        }

        return fotosSubidas;
    }
}

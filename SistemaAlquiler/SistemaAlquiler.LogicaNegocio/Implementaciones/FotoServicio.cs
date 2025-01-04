using Microsoft.AspNetCore.Http;
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
        _rutaBase = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos"); // Ruta local
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
        var foto = await repositorio.obtener(u=> u.idFoto==idFoto); // Método que obtiene la foto por Id
        if (foto == null)
        {
            return null; // O lanza una excepción si no se encuentra
        }
        Foto temp = foto.FirstOrDefault();
        // Elimina la foto del sistema de archivos
        var filePath = Path.Combine(_rutaBase, Path.GetFileName(temp.direccionName));
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
        var foto = await repositorio.obtener(u => u.idCasa == idCasa); // Método que obtiene la foto por Id
        if (foto.ToList().Count==0)
        {
            return null; // O lanza una excepción si no se encuentra
        }
        List<Foto> temp = foto.ToList();
        // Elimina la foto del sistema de archivos
        foreach(Foto f in temp)
        {
            if (File.Exists(f.direccionURL))
            {
                File.Delete(f.direccionURL);
            }
            await repositorio.eliminar(f);
        }
        
        return temp; // Devuelve la foto eliminada
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

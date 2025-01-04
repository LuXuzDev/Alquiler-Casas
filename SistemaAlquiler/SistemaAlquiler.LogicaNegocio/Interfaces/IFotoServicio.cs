using Microsoft.AspNetCore.Http;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAlquiler.LogicaNegocio.Interfaces;

public interface IFotoServicio
{
    Task<List<Foto>> subirFoto (List<IFormFile> fotos, int idCasa);
    Task<List<string>> bajarFotosCasa(int idCasa);
    Task<Foto> eliminarFoto(int idFoto);
    Task<List<Foto>> eliminarFotos(int idCasa);
    Task llenarDTOs(List<CasaDTO> casaDTOs);
    Task llenarDTO(CasaDTO casaDTO);
}

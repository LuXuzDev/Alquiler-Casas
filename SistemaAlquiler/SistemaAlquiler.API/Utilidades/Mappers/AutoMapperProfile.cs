using SistemaAlquiler.Entidades;
using AutoMapper;
using SistemaAlquiler.API.DTOs;

namespace SistemaAlquiler.API.Utilidades.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() 
    {
        #region Usuario   
        CreateMap<Usuario,UsuarioDTO>().ReverseMap();
        #endregion

        #region Casa
        CreateMap<Casa, CasaDTO>()
        .ForMember(dest => dest.ciudad , opt => opt.MapFrom(src => src.ciudad.ciudad))
        .ForMember(dest => dest.correo, opt => opt.MapFrom(src => src.usuario.correo))
        .ForMember(dest => dest.numeroContacto, opt => opt.MapFrom(src => src.usuario.numeroContacto))
        .ForMember(dest => dest.caracteristicas, opt => opt.MapFrom(src => src.caracteristicas))
        .ReverseMap();
        #endregion

        #region Ciudad
        CreateMap<Ciudad, CiudadDTO>().ReverseMap();
        #endregion
    }
}

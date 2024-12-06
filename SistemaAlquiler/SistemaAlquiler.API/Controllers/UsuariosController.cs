using Microsoft.AspNetCore.Mvc;
using SistemaAlquiler.AccesoDatos;
using SistemaAlquiler.Entidades;
using SistemaAlquiler.LogicaNegocio.Interfaces;
using SistemaAlquiler.API.Utilidades.Mappers;
using AutoMapper;
using SistemaAlquiler.LogicaNegocio.DTOs;

namespace SistemaAlquiler.API.Controllers;

[ApiController]
[Route("usuarios/")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioServicio usuarioServicio;

    private readonly IMapper autoMapper;

    public UsuariosController(IUsuarioServicio usuarioServicio, IMapper autoMapper)
    {
        this.usuarioServicio = usuarioServicio;

        this.autoMapper = autoMapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> usuarioId([FromBody] int id)
    {
        var usuario = await usuarioServicio.obtenerPorId(id);
        UsuarioDTO vmUsuario = autoMapper.Map<UsuarioDTO>(usuario);
        return StatusCode(StatusCodes.Status200OK, vmUsuario);
    }


    [HttpGet("listaUsuarios")]
    public async Task<IActionResult> listaUsuarios()
    {
        var listaUsuarios = await usuarioServicio.lista();
        List<UsuarioDTO> vmLista = autoMapper.Map<List<UsuarioDTO>>(listaUsuarios);
        return StatusCode(StatusCodes.Status200OK, vmLista);
    }


    [HttpGet("listaGestores")]
    public async Task<IActionResult> listaGestores()
    {
        var listaGestores = await usuarioServicio.obtenerPorRol(2);
        List<UsuarioDTO> vmLista = autoMapper.Map<List<UsuarioDTO>>(listaGestores);
        return StatusCode(StatusCodes.Status200OK, vmLista);

    }

    [HttpPost("crear")]
    public async Task<IActionResult> crear([FromBody] CrearUsuarioDTO user)
    {
        Usuario u = await usuarioServicio.crear(user);
        UsuarioDTO vmUsuario = autoMapper.Map<UsuarioDTO>(u);
        return StatusCode(StatusCodes.Status200OK, vmUsuario);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> eliminar([FromBody] int idUsuario)
    {
        var eliminado = usuarioServicio.eliminar(idUsuario);
        return StatusCode(StatusCodes.Status200OK);
    }


    [HttpPatch("editar")]
    public async Task<IActionResult> editar([FromBody] int idUsuario,string? correo,string? numeroContacto,string? clave)
    {
        Usuario actualizado = await usuarioServicio.editar(correo,numeroContacto,clave, idUsuario);
        UsuarioDTO vmUsuario = autoMapper.Map<UsuarioDTO>(actualizado);
        return StatusCode(StatusCodes.Status200OK, vmUsuario);

    }

    [HttpPatch("cambiarRol (\"{id}\")")]
    public async Task<IActionResult> cambiarRol([FromBody] int idUsuario,int idRol)
    {
        Usuario actualizado = await usuarioServicio.editarRol(idUsuario,idRol);
        UsuarioDTO vmUsuario = autoMapper.Map<UsuarioDTO>(actualizado);
        return StatusCode(StatusCodes.Status200OK, vmUsuario);
    }

}

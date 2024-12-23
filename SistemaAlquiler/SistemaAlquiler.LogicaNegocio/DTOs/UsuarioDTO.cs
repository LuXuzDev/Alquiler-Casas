namespace SistemaAlquiler.LogicaNegocio.DTOs
{
    public class UsuarioDTO
    {
        public int idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string correo { get; set; }
        public string numeroContacto { get; set; }
        public int idRol { get; set; }
    }
}

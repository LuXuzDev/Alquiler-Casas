using Microsoft.EntityFrameworkCore;
using SistemaAlquiler.Entidades;

namespace SistemaAlquiler.AccesoDatos
{
    public class DB_Context : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Casa> Casas { get; set; }
        public DbSet<Caracteristicas> Caracteristicas { get; set; }
        public DbSet<Foto> Fotos { get; set; }
        public DbSet<Reservacion> Reservaciones { get; set; }
        public DbSet<Valoracion> Valoraciones { get; set; }
        public DB_Context(DbContextOptions<DB_Context> options) : base(options)
        {

        }
    }
}

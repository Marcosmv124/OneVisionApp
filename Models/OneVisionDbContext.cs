using Microsoft.EntityFrameworkCore;

namespace One_Vision.Models
{
    public class OneVisionDbContext : DbContext
    {
        public OneVisionDbContext(DbContextOptions<OneVisionDbContext> options)
            : base(options)
        {
        }

        // DbSet en singular o plural como prefieras, aquí lo dejamos plural:
        public DbSet<Historial> Historiales { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta>  Ventas {get; set; }
        public DbSet<VentaProducto> VentaProductos {get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
        }
    }
}

using GameStoreApi.Models;
using Microsoft.EntityFrameworkCore;


namespace GameStoreApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Rol> Roles => Set<Rol>();
        public DbSet<Categoria> Categorias => Set<Categoria>();
        public DbSet<Videojuego> Videojuegos => Set<Videojuego>();
        public DbSet<Inventario> Inventarios => Set<Inventario>();
        public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();


        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);


            mb.Entity<Usuario>()
            .HasIndex(u => u.Username)
            .IsUnique();


            mb.Entity<Videojuego>()
            .HasOne(v => v.Inventario)
            .WithOne(i => i.Videojuego)
            .HasForeignKey<Inventario>(i => i.VideojuegoId);


            // Seed básico de roles
            mb.Entity<Rol>().HasData(
            new Rol { Id = 1, Nombre = "Admin" },
            new Rol { Id = 2, Nombre = "Vendedor" }
            );
        }
    }
}
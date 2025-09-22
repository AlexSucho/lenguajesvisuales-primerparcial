using BCrypt.Net;
using GameStoreApi.Models;
using Microsoft.EntityFrameworkCore;


namespace GameStoreApi.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            await db.Database.MigrateAsync();


            if (!await db.Usuarios.AnyAsync())
            {
                var admin = new Usuario
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
                    NombreCompleto = "Administrador",
                    RolId = 1
                };
                db.Usuarios.Add(admin);
            }


            if (!await db.Categorias.AnyAsync())
            {
                db.Categorias.AddRange(
                new Categoria { Nombre = "Acción" },
                new Categoria { Nombre = "Deportes" },
                new Categoria { Nombre = "RPG" }
                );
            }


            await db.SaveChangesAsync();
        }
    }
}

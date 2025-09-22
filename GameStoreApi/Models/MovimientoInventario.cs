using System.ComponentModel.DataAnnotations;


namespace GameStoreApi.Models
{
    public class MovimientoInventario
    {
        public int Id { get; set; }
        public int VideojuegoId { get; set; }
        public Videojuego Videojuego { get; set; } = null!;
        public int Cantidad { get; set; } // +ingreso / -salida
        [MaxLength(200)]
        public string? Motivo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
    }
}
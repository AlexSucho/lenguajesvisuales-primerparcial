using System.Text.Json.Serialization;

namespace GameStoreApi.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        public int VideojuegoId { get; set; }

        [JsonIgnore] // evita el ciclo Videojuego -> Inventario -> Videojuego ...
        public Videojuego Videojuego { get; set; } = null!;

        public int Stock { get; set; }
    }
}

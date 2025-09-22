namespace GameStoreApi.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!; // Accion, Deportes, RPG, etc.
        public string? Descripcion { get; set; }


        public ICollection<Videojuego> Videojuegos { get; set; } = new List<Videojuego>();
    }
}

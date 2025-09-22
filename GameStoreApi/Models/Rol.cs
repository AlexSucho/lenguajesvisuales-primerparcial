namespace GameStoreApi.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!; // Admin, Vendedor


        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
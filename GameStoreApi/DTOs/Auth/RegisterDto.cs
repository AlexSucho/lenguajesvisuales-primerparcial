namespace GameStoreApi.DTOs.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? NombreCompleto { get; set; }
        public int RolId { get; set; } = 2; // por defecto Vendedor
    }
}
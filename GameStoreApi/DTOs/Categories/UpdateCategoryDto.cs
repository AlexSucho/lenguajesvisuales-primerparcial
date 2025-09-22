namespace GameStoreApi.DTOs.Categories
{
    public class UpdateCategoryDto
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
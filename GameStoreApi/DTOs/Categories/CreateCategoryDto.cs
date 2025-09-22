namespace GameStoreApi.DTOs.Categories
{
    public class CreateCategoryDto
    {
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}

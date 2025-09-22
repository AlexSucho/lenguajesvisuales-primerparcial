namespace GameStoreApi.DTOs.Games
{
    public class CreateGameDto
    {
        public string Titulo { get; set; } = null!;
        public string? Plataforma { get; set; }
        public string? Editorial { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        public int StockInicial { get; set; } = 0;
    }
}

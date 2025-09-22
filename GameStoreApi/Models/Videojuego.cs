using GameStoreApi.Models;
using Microsoft.EntityFrameworkCore; // <— agrega esto

public class Videojuego
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Plataforma { get; set; }
    public string? Editorial { get; set; }

    [Precision(18, 2)]               // <— precisión/escala recomendada
    public decimal Precio { get; set; }

    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; } = null!;
    public Inventario Inventario { get; set; } = null!;
}
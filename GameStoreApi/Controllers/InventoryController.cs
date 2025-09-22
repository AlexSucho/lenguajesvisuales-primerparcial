using GameStoreApi.Data;
using GameStoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GameStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public InventoryController(ApplicationDbContext db) { _db = db; }


        public record AdjustDto(int VideojuegoId, int Cantidad, string? Motivo);


        [HttpPost("adjust")]
        [Authorize]
        public async Task<IActionResult> Adjust(AdjustDto dto)
        {
            var juego = await _db.Videojuegos.Include(v => v.Inventario).FirstOrDefaultAsync(v => v.Id == dto.VideojuegoId);
            if (juego is null) return NotFound();


            juego.Inventario.Stock += dto.Cantidad;
            var userId = int.Parse(User.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier || c.Type == "sub").Value);


            _db.MovimientosInventario.Add(new MovimientoInventario
            {
                VideojuegoId = juego.Id,
                Cantidad = dto.Cantidad,
                Motivo = dto.Motivo,
                UsuarioId = userId
            });
            await _db.SaveChangesAsync();
            return Ok(new { juego.Id, juego.Titulo, StockActual = juego.Inventario.Stock });
        }


        [HttpGet("movements/{gameId:int}")]
        public async Task<IActionResult> Movements(int gameId)
        {
            var movs = await _db.MovimientosInventario
            .Where(m => m.VideojuegoId == gameId)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();
            return Ok(movs);
        }
    }
}
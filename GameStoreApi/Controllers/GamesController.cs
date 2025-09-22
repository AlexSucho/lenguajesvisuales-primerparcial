using GameStoreApi.Data;
using GameStoreApi.DTOs.Games;
using GameStoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GamesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public GamesController(ApplicationDbContext db) { _db = db; }

        // GET api/games?search=pes&categoryId=2&page=1&pageSize=10
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int? categoryId,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var q = _db.Videojuegos
                .Include(v => v.Categoria)
                .Include(v => v.Inventario)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(v => v.Titulo.Contains(search));
            if (categoryId.HasValue)
                q = q.Where(v => v.CategoriaId == categoryId.Value);

            var total = await q.CountAsync();

            var items = await q.OrderBy(v => v.Titulo)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(v => new
                {
                    v.Id,
                    v.Titulo,
                    v.Plataforma,
                    v.Editorial,
                    v.Precio,
                    v.CategoriaId,
                    Categoria = v.Categoria.Nombre,
                    Stock = v.Inventario.Stock
                })
                .ToListAsync();

            return Ok(new { total, page, pageSize, items });
        }

        // GET api/games/5
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var v = await _db.Videojuegos
                .Include(x => x.Categoria)
                .Include(x => x.Inventario)
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.Titulo,
                    x.Plataforma,
                    x.Editorial,
                    x.Precio,
                    x.CategoriaId,
                    Categoria = x.Categoria.Nombre,
                    Stock = x.Inventario.Stock
                })
                .FirstOrDefaultAsync();

            return v is null ? NotFound() : Ok(v);
        }

        // POST api/games  (Admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateGameDto dto)
        {
            var v = new Videojuego
            {
                Titulo = dto.Titulo,
                Plataforma = dto.Plataforma,
                Editorial = dto.Editorial,
                Precio = dto.Precio,
                CategoriaId = dto.CategoriaId,
                Inventario = new Inventario { Stock = dto.StockInicial }
            };

            _db.Videojuegos.Add(v);
            await _db.SaveChangesAsync();

            var result = new
            {
                v.Id,
                v.Titulo,
                v.Plataforma,
                v.Editorial,
                v.Precio,
                v.CategoriaId,
                Stock = v.Inventario.Stock
            };

            return CreatedAtAction(nameof(Get), new { id = v.Id }, result);
        }

        // PUT api/games/5  (Admin)
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateGameDto dto)
        {
            var v = await _db.Videojuegos
                .Include(x => x.Inventario)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (v is null) return NotFound();

            v.Titulo = dto.Titulo;
            v.Plataforma = dto.Plataforma;
            v.Editorial = dto.Editorial;
            v.Precio = dto.Precio;
            v.CategoriaId = dto.CategoriaId;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/games/5  (Admin)
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var v = await _db.Videojuegos.FindAsync(id);
            if (v is null) return NotFound();

            _db.Videojuegos.Remove(v);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

using GameStoreApi.Data;
using GameStoreApi.DTOs.Categories;
using GameStoreApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GameStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public CategoriesController(ApplicationDbContext db) { _db = db; }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll() => Ok(await _db.Categorias.ToListAsync());


        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var cat = await _db.Categorias.FindAsync(id);
            return cat is null ? NotFound() : Ok(cat);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            var cat = new Categoria { Nombre = dto.Nombre, Descripcion = dto.Descripcion };
            _db.Categorias.Add(cat);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cat.Id }, cat);
        }


        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, UpdateCategoryDto dto)
        {
            var cat = await _db.Categorias.FindAsync(id);
            if (cat is null) return NotFound();
            cat.Nombre = dto.Nombre; cat.Descripcion = dto.Descripcion;
            await _db.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var cat = await _db.Categorias.FindAsync(id);
            if (cat is null) return NotFound();
            _db.Categorias.Remove(cat);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
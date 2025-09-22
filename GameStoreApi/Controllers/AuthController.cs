using BCrypt.Net;
using GameStoreApi.Data;
using GameStoreApi.DTOs.Auth;
using GameStoreApi.Models;
using GameStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GameStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly JwtService _jwt;
        public AuthController(ApplicationDbContext db, JwtService jwt)
        {
            _db = db; _jwt = jwt;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _db.Usuarios.AnyAsync(u => u.Username == dto.Username))
                return BadRequest(new { message = "El usuario ya existe" });


            var user = new Usuario
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                NombreCompleto = dto.NombreCompleto,
                RolId = dto.RolId
            };
            _db.Usuarios.Add(user);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Registrado" });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _db.Usuarios.Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized();


            var token = _jwt.CreateToken(user);
            return Ok(new { token });
        }
    }
}
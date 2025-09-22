using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameStoreApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace GameStoreApi.Services
{
    public class JwtOptions
    {
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int DurationMinutes { get; set; }
    }


    public class JwtService
    {
        private readonly JwtOptions _opt;
        public JwtService(IOptions<JwtOptions> opt) { _opt = opt.Value; }


        public string CreateToken(Usuario user)
        {
            var claims = new List<Claim>
{
new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
new Claim(ClaimTypes.Role, user.RolId == 1 ? "Admin" : "Vendedor")
};


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: _opt.Issuer,
            audience: _opt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_opt.DurationMinutes),
            signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
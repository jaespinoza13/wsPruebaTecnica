using Application.Interfaz;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebUI.Middleware;

public class Autorizacion : IAuthService
{
    private readonly IConfiguration _configuration;

    public Autorizacion(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<string> AuthenticateAsync(string username)
    {
        var claims = new[]
{
        new Claim(ClaimTypes.Name, username),
        new Claim(ClaimTypes.Role, "Usuario") // Puedes cambiar el rol si es necesario
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}


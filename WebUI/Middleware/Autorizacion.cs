using Application.Interfaz;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebUI.Middleware;

public class Autorizacion : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthenticationService> _logger;

    public Autorizacion(IConfiguration configuration, ILogger<AuthenticationService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }
    public async Task<string> AuthenticateAsync(string username)
    {
        _logger.LogInformation("Generando token para el usuario: {Username}", username);

        try
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Usuario")
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

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            _logger.LogInformation("Token generado con éxito para {Username}", username);

            return tokenString;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generando token para {Username}", username);
            throw;
        }
    }
}


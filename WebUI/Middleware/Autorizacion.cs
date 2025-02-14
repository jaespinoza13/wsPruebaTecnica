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
    public async Task<string> AuthenticateAsync(string username, string password)
    {
        // Lógica para validar las credenciales (esto depende de tu lógica de autenticación)
        // Aquí puedes realizar la verificación con la base de datos o cualquier otra fuente de autenticación

        if (username == "validUser" && password == "validPassword") // Este es un ejemplo de validación
        {
            // Si la validación es exitosa, generamos el token JWT
            var claims = new[] {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Usuario")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // Retorna el token generado
        }
        else
        {
            throw new UnauthorizedAccessException("Credenciales incorrectas"); // Lanza un error si las credenciales son incorrectas
        }
    }
}


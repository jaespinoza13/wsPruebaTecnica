using Application.Interfaz;
using Application.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Infrastructure.Persistence;


public class ValidarUsuarioDat : IValidarUsuarioDat
{
    private readonly string _connectionString;

    public ValidarUsuarioDat(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public async Task<ResValidarUsuario> GetValidacionUsuario(ReqValidarUsuario request)
{
    using (var connection = new SqlConnection(_connectionString))
    {
        try
        {
            await connection.OpenAsync();

            var query = @"SELECT Id, Usuario FROM Usuarios WHERE Usuario = @Usuario AND Pass = @Pass";

            var usuario = await connection.QueryFirstOrDefaultAsync<ResValidarUsuario>(
                query,
                new { Usuario = request.UsuarioNombre, Pass = request.Pass, Mensaje = "Autenticación exitosa" }
            );

            if (usuario != null)
            {
                return new ResValidarUsuario
                {
                    Id = usuario.Id,
                    Mensaje = "Autenticación exitosa"
                };
            }
            return new ResValidarUsuario { Mensaje = "Credenciales incorrectas" };
            }
        catch (Exception ex)
        {
            return new ResValidarUsuario { Mensaje = $"Error: {ex.Message}" };
        }
    }
}


}
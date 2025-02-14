using Application.Interfaz;
using Application.Login;
using Application.Registro.AgregarPersona;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class RegistroPersonasDat : IRegistroPersonasDat
{
    private readonly string _connectionString;

    public RegistroPersonasDat(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    public async Task<ResRegistrarPersona> AgregarPersona(ReqRegistrarPersona request)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            try
            {
                // Verificar si la persona ya existe en la base de datos
                var existePersona = await connection.ExecuteScalarAsync<int>(
                    "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @Usuario",
                    new { Usuario = request.obj_persona.Apellidos }
                );

                if (existePersona > 0)
                {
                    return new ResRegistrarPersona { Mensaje = "La persona ya está registrada" };
                }

                // Insertar nueva persona
                var query = @"
                INSERT INTO Usuarios (Usuario, Pass) 
                VALUES (@Usuario, @Pass);
                ";

                var filasAfectadas = await connection.ExecuteAsync(
                    query,
                    new { Usuario = request.obj_persona.Apellidos, Pass = request.obj_persona.Apellidos }
                );

                if (filasAfectadas > 0)
                {
                    return new ResRegistrarPersona { Mensaje = "Registro exitoso" };
                }
                else
                {
                    return new ResRegistrarPersona { Mensaje = "Error al registrar la persona" };
                }

            }
            catch (Exception ex)
            {
                return new ResRegistrarPersona { Mensaje = $"Error inesperado: {ex.Message}" };
            }
        }
    }
}
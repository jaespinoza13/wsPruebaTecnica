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
            await connection.OpenAsync();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Verificar si la persona ya existe en la base de datos (validando por Número de Identificación)
                    var existePersona = await connection.ExecuteScalarAsync<int>(
                        "SELECT COUNT(*) FROM Personas WHERE NumeroIdentificacion = @NumeroIdentificacion",
                        new { NumeroIdentificacion = request.obj_persona.NumeroIdentificacion },
                        transaction
                    );

                    if (existePersona > 0)
                    {
                        return new ResRegistrarPersona { Mensaje = "La persona ya está registrada" };
                    }

                    // Insertar en la tabla Personas
                    var queryPersona = @"
                    INSERT INTO Personas (Nombres, Apellidos, NumeroIdentificacion, TipoIdentificacion, Email, FechaCreacion)
                    VALUES (@Nombres, @Apellidos, @NumeroIdentificacion, @TipoIdentificacion, @Email, @FechaCreacion);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);";


                    await connection.ExecuteAsync(queryPersona, new
                    {
                        request.obj_persona.Nombres,
                        request.obj_persona.Apellidos,
                        request.obj_persona.NumeroIdentificacion,
                        request.obj_persona.Email,
                        request.obj_persona.TipoIdentificacion,
                        FechaCreacion = DateTime.UtcNow,
                        NumeroCompleto = $"{request.obj_persona.TipoIdentificacion}-{request.obj_persona.NumeroIdentificacion}",
                        NombreCompleto = $"{request.obj_persona.Nombres} {request.obj_persona.Apellidos}"
                    }, transaction);

                    // Insertar en la tabla Usuarios
                    var queryUsuario = @"
                INSERT INTO Usuarios (Usuario, Pass, FechaCreacion) 
                VALUES (@Usuario, @Pass, @FechaCreacion);
                ";

                    await connection.ExecuteAsync(queryUsuario, new
                    {
                        Usuario = request.obj_persona.Usuario,
                        Pass = request.obj_persona.Pass, // Debería estar encriptada antes de guardarla
                        FechaCreacion = DateTime.UtcNow
                    }, transaction);

                    // Confirmar la transacción
                    transaction.Commit();

                    return new ResRegistrarPersona { Mensaje = "Registro exitoso" };
                }
                catch (Exception ex)
                {
                    // Revertir cambios en caso de error
                    transaction.Rollback();
                    return new ResRegistrarPersona { Mensaje = $"Error inesperado: {ex.Message}" };
                }
            }
        }
    }

}

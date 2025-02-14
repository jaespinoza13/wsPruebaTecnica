using Application.Interfaz;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Application.Login;

public class ValidarUsuarioHandler : IRequestHandler<ReqValidarUsuario, ResValidarUsuario>
{
    private readonly string _clase;
    private readonly IValidarUsuarioDat _validarUsuario;
    private readonly ILogger<ValidarUsuarioHandler> _logger;

    public ValidarUsuarioHandler(IValidarUsuarioDat validarUsuarioDat, ILogger<ValidarUsuarioHandler> logger)
    {
        _validarUsuario = validarUsuarioDat;
        _clase = GetType().Name;
        _logger = logger;

    }
    public async Task<ResValidarUsuario> Handle(ReqValidarUsuario request, CancellationToken cancellationToken) 
    {
        try
         {
            _logger.LogInformation("Se valida el ingreso al handler para validar el usuario: Id={Id}, Usuario={Usuario}, FechaCreacion={FechaCreacion}",
            request.Id, request.UsuarioNombre, request.FechaCreacion);

            var response = await _validarUsuario.GetValidacionUsuario(request);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Se Presento un error en el handler para validar un usuario");
            return new ResValidarUsuario { Mensaje = $"Error: {ex.Message}" };
        }
    
    }
}

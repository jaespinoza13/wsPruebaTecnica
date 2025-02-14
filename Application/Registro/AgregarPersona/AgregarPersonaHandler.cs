using Application.Interfaz;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Registro.AgregarPersona;

public class AgregarPersonaHandler : IRequestHandler<ReqRegistrarPersona, ResRegistrarPersona>
{
    private readonly string _clase;
    private readonly IRegistroPersonasDat _registrarPersona;
    private readonly ILogger<AgregarPersonaHandler> _logger;

    public AgregarPersonaHandler(IRegistroPersonasDat registroPersonasDat, ILogger<AgregarPersonaHandler> logger)
    {
        _registrarPersona = registroPersonasDat;
        _logger = logger;
    }

    public async Task<ResRegistrarPersona> Handle(ReqRegistrarPersona request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Se valida el ingreso al handler para registrar una persona: Id={Id}, Nombre={NombreCompleto}, Identificación={IdentificacionCompleta}, Email={Email}, FechaCreacion={FechaCreacion}",
    request.obj_persona.Id, request.obj_persona.NombreCompleto, request.obj_persona.IdentificacionCompleta, request.obj_persona.Email, request.obj_persona.FechaCreacion);

            var response = await _registrarPersona.AgregarPersona(request);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Se Presento un error en el handler para registrar una persona");
            throw;
        }

    }
}

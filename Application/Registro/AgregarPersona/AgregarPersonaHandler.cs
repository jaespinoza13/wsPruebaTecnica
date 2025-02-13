using Application.Interfaz;
using Application.Login;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Registro.AgregarPersona;

public class AgregarPersonaHandler : IRequestHandler<ReqRegistrarPersona, ResRegistrarPersona>
{
    //public readonly ILogs _logsService;
    private readonly string _clase;
    private readonly IRegistroPersonasDat _registrarPersona;

    public AgregarPersonaHandler(IRegistroPersonasDat registroPersonasDat)
    {
        _registrarPersona = registroPersonasDat;
    }

    public async Task<ResRegistrarPersona> Handle(ReqRegistrarPersona request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _registrarPersona.AgregarPersona(request);
            return response;
        }
        catch (Exception)
        {

            throw;
        }

    }
}

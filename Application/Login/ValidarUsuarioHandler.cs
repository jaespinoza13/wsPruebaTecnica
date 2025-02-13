using Application.Interfaz;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Application.Login;

public class ValidarUsuarioHandler : IRequestHandler<ReqValidarUsuario, ResValidarUsuario>
{
    //public readonly ILogs _logsService;
    private readonly string _clase;
    private readonly IValidarUsuarioDat _validarUsuario;

    public ValidarUsuarioHandler(IValidarUsuarioDat validarUsuarioDat)
    {
        _validarUsuario = validarUsuarioDat;
        _clase = GetType().Name;

    }
    public async Task<ResValidarUsuario> Handle(ReqValidarUsuario request, CancellationToken cancellationToken) 
    {
        try
        {
            var response = await _validarUsuario.GetValidacionUsuario(request);
            return response;
        }
        catch (Exception)
        {

            throw;
        }
    
    }
}

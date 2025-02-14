using Application.Registro.AgregarPersona;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Route("api/personas")]
    [ApiController]
    [Authorize]  
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class ApiPersonasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiPersonasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("registro")]
        [Produces("application/json")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] ReqRegistrarPersona request)
        {
            try
            {
                var response = await _mediator.Send(request);

                return response.Mensaje switch
                {
                    "La persona ya está registrada" => Conflict(new { Mensaje = response.Mensaje }),
                    "Registro exitoso" => Created("", response),
                    _ => BadRequest(new { Mensaje = response.Mensaje })
                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error inesperado: {ex.Message}" });
            }
        }
    }
}

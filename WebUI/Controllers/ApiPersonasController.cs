using Application.Login;
using Application.Registro.AgregarPersona;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Route("api/personas")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ApiPersonasController : Controller
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


                if (response.Mensaje == "La persona ya está registrada")
                {
                    return Conflict(new { exito = false, mensaje = response.Mensaje });
                }

                if (response.Mensaje == "Registro exitoso")
                {
                    return Ok(new { exito = true, mensaje = response.Mensaje });
                }

                return BadRequest(response); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Error inesperado: {ex.Message}" });
            }
        }
    }
}

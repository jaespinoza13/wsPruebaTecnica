using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Login;
using MediatR;


namespace WebUI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ApiUsuarioController : Controller
    {

        private readonly IMediator _mediator;

        // Inyectar IMediator en el constructor
        public ApiUsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<IActionResult> Usuarios(ReqValidarUsuario request)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (response.Mensaje == "Credenciales incorrectas")
                {
                    return Unauthorized(response); // Retorna una respuesta de Unauthorized si las credenciales son incorrectas
                }

                return Ok(response); // Retorna una respuesta exitosa si las credenciales son correctas
            }
            catch (Exception ex)
            {
                // Manejo global de excepciones
                return StatusCode(500, new { Mensaje = $"Hubo un error: {ex.Message}" });
            }
        }
    }
}

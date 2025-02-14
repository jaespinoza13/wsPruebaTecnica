using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Login;
using Application.Interfaz;
using MediatR;
using System;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ApiUsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;

        public ApiUsuarioController(IMediator mediator, IAuthService authService)
        {
            _mediator = mediator;
            _authService = authService;
        }

        [HttpPost("login")]
        [Produces("application/json")]
        public async Task<IActionResult> Login(ReqValidarUsuario request)
        {
            try
            {
                var response = await _mediator.Send(request);

                if (response.Mensaje == "Autenticación exitosa")
                {
                    var token = await _authService.AuthenticateAsync(request.UsuarioNombre);

                    return Ok(new
                    {
                        response = response.Mensaje,
                        token = token
                    });
                }
                return Unauthorized(new { Mensaje = response.Mensaje });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { Mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Mensaje = $"Hubo un error: {ex.Message}" });
            }
        }
    }
}

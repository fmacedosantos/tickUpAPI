using api.Requests;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TickUp.Services;

namespace TickUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly EventoService _eventoService;

        public EventoController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }

        /// <summary>
        /// Realiza o login de um administrador usando o email de contato e o ID do evento.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST /api/Evento/Login
        ///     {
        ///        "emailContato": "email@example.com",
        ///        "idEvento": "37"
        ///     }
        /// </remarks>
        /// <param name="loginRequest">Dados de login contendo o email de contato e o ID do evento</param>
        /// <returns>Um valor booleano indicando se o login foi bem-sucedido</returns>
        /// <response code="200">Se o login foi realizado com sucesso</response>
        /// <response code="400">Se os dados de login forem inválidos</response>
        /// <response code="401">Se o email de contato ou ID do evento forem inválidos</response>
        [HttpPost("Login")]
        [SwaggerOperation(Summary = "Realiza o login de um evento", Description = "Realiza o login de um evento usando o email de contato e o ID do evento.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(bool))]
        public IActionResult Login([FromBody] LoginEventoRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(false);  
            }

            var resultado = _eventoService.Login(loginRequest.EmailContato, loginRequest.IdEvento);
            if (resultado)
            {
                return Ok(true);  
            }
            else
            {
                return Unauthorized(false); 
            }
        }
    }
}

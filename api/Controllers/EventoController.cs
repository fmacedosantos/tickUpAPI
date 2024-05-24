using api.Requests;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginEventoRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Dados de login inválidos.");
            }

            var resultado = _eventoService.Login(loginRequest.EmailContato, loginRequest.IdEvento);
            if (resultado)
            {
                return Ok("Login de evento realizado com sucesso.");
            }
            else
            {
                return Unauthorized("Email de contato ou ID de evento inválidos.");
            }
        }
    }
}

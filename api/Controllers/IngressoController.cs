using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngressoController : ControllerBase
    {
        private readonly IngressoService _ingressoService;

        public IngressoController(IngressoService ingressoService)
        {
            _ingressoService = ingressoService;
        }

        [HttpGet("Verificar/{idIngresso}")]
        public IActionResult VerificarIngresso(string idIngresso)
        {
            try
            {
                bool ingressoValido = _ingressoService.VerificarExistencia(idIngresso);
                if (ingressoValido)
                {
                    return Ok($"O ingresso com o ID {idIngresso} é válido.");
                }
                else
                {
                    return NotFound($"O ingresso com o ID {idIngresso} não foi encontrado ou não é válido.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao verificar o ingresso: {ex.Message}");
            }
        }
    }
}

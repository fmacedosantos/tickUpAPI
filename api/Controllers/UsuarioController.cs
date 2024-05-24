using api.Models;
using api.Requests;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Cadastrar")]
        public IActionResult Cadastrar([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Dados inválidos.");
            }

            try
            {
                string mensagem = _usuarioService.InserirUsuario(usuario);
                return Ok(mensagem);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar usuário: {ex.Message}");
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUsuarioRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest(false);
            }

            var resultado = _usuarioService.Login(loginRequest);
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

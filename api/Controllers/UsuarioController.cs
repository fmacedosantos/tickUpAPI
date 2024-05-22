using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult Cadastrar(string emailUser, string cpfUser, string nomeUser, string telefoneUser, string senhaUser, int idadeUser)
        {
            Usuario user = new Usuario(emailUser, cpfUser, nomeUser, telefoneUser, senhaUser, idadeUser);
            user.InserirUsuario();
            return Ok(user);
        }
    }
}

using api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

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

        /// <summary>
        /// Verifica se o ingresso, apresentado em forma de QR code, é válido.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     GET /api/Ingresso/Verificar/{idIngresso}
        /// 
        /// </remarks>
        /// <param name="idIngresso">O ID do ingresso a ser verificado</param>
        /// <returns>Um valor booleano indicando se o ingresso é válido</returns>
        /// <response code="200">Se a verificação foi bem-sucedida e o ingresso é válido</response>
        /// <response code="404">Se o ingresso não for encontrado ou não for válido</response>
        /// <response code="500">Se houver um erro no servidor</response>
        [HttpGet("Verificar/{idIngresso}")]
        [SwaggerOperation(Summary = "Verifica a validade de um ingresso", Description = "Verifica se o ingresso, apresentado em forma de QR code, é válido.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult VerificarIngresso(string idIngresso)
        {
            try
            {
                bool ingressoValido = _ingressoService.VerificarExistencia(idIngresso);
                if (ingressoValido)
                {
                    return Ok(true);  
                }
                else
                {
                    return NotFound(false);  
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, false);  
            }
        }
    }
}

using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Obtém todos os ingressos associados a um determinado email.
        /// </summary>
        /// <param name="email">O email do usuário cujos ingressos serão obtidos</param>
        /// <returns>Uma lista de ingressos</returns>
        /// <response code="200">Se a obtenção dos ingressos foi bem-sucedida</response>
        /// <response code="500">Se houver um erro no servidor</response>
        [HttpGet("Usuario/{email}")]
        public IActionResult ObterIngressosPorEmail(string email)
        {
            try
            {
                List<Ingresso> ingressos = _ingressoService.ObterIngressosPorEmail(email);
                if (ingressos != null)
                {
                    return Ok(ingressos);
                }
                else
                {
                    return Ok(new List<Ingresso>());
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os ingressos do usuário: {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta um ingresso específico pelo seu ID.
        /// </summary>
        /// <param name="idIngresso">O ID do ingresso a ser deletado</param>
        /// <returns>Status da operação de exclusão</returns>
        /// <response code="200">Se a exclusão foi bem-sucedida</response>
        /// <response code="404">Se o ingresso não for encontrado</response>
        /// <response code="500">Se houver um erro no servidor</response>
        [HttpDelete("Deletar/{idIngresso}")]
        [SwaggerOperation(Summary = "Deleta um ingresso pelo ID", Description = "Deleta um ingresso específico pelo seu ID.")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletarIngresso(string idIngresso)
        {
            try
            {
                _ingressoService.DeletarIngresso(idIngresso);
                return Ok();  
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Nenhum ingresso foi encontrado"))
                {
                    return NotFound($"Ingresso com ID {idIngresso} não encontrado.");
                }
                return StatusCode(500, $"Erro ao deletar o ingresso: {ex.Message}");
            }
        }
    }
}

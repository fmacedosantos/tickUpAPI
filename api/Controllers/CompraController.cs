using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraController : ControllerBase
    {
        private readonly CompraService _compraService;

        public CompraController(CompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet("Usuario/{email}")]
        public IActionResult ObterIngressosPorEmail(string email)
        {
            // Apresenta os ingressos comprados pelo usuário.
            try
            {
                var idsIngressos = _compraService.ObterIngressosPorEmail(email);
                if (idsIngressos != null)
                {
                    return Ok(idsIngressos);
                }
                else
                {
                    return NotFound($"O usuário cadastrado não possui ingressos.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os ingressos do usuário: {ex.Message}");
            }
        }
    }
}

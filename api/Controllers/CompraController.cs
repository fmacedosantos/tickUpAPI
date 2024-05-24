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
            try
            {
                var idsIngressos = _compraService.ObterIngressosPorEmail(email);
                return Ok(idsIngressos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os ingressos do usuário: {ex.Message}");
            }
        }
    }
}

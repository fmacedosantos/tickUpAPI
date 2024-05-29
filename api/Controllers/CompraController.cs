using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
                List<Ingresso> ingressos = _compraService.ObterIngressosPorEmail(email);
                if (ingressos != null && ingressos.Count > 0)
                {
                    return Ok(ingressos);
                }
                else
                {
                    return NotFound("O usuário cadastrado não possui ingressos.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os ingressos do usuário: {ex.Message}");
            }
        }
    }
}

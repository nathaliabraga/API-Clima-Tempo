using ClimaSprint.Application.Dtos;
using ClimaSprint.Domain.Entities;
using ClimaSprint.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ClimaSprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimaController : ControllerBase
    {
        private readonly IClimaApplicationService _applicationService;

        public ClimaController(IClimaApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet("atual/{cidade}")]
        [Produces<ClimaEntity>]
        public async Task<IActionResult> GetClimaAtual(string cidade)
        {
            try
            {
                var climaAtual = await _applicationService.ObterClimaAtualAsync(cidade);

                if (climaAtual is not null)
                    return Ok(climaAtual);

                return NotFound("Dados do clima não encontrados para a cidade informada.");
            }
            catch (Exception ex) 
            {
               
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro ao obter clima atual: {ex.Message}");
            }
        }

        [HttpGet("previsao/{cidade}")]
        [Produces<IEnumerable<PrevisaoEntity>>]
        public async Task<IActionResult> GetPrevisao(string cidade)
        {
            try
            {
                var previsao = await _applicationService.ObterPrevisaoAsync(cidade);

                if (previsao is not null)
                    return Ok(previsao);

                return NotFound("Previsão do tempo não encontrada para a cidade informada.");
            }
            catch (Exception ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro ao obter previsão do tempo: {ex.Message}");
            }
        }

        [HttpGet("historico/{cidade}")]
        [Produces<IEnumerable<HistoricoClimaEntity>>]
        public async Task<IActionResult> GetHistoricoClima(string cidade)
        {
            try
            {
                var historico = await _applicationService.ObterHistoricoClimaAsync(cidade);

                if (historico is not null)
                    return Ok(historico);

                return NotFound("Histórico do clima não encontrado para a cidade informada.");
            }
            catch (Exception ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro ao obter histórico do clima: {ex.Message}");
            }
        }

        [HttpGet("busca/endereco/{cep}")]
        [Produces<Endereco>]
        public async Task<IActionResult> GetEndereco(string cep)
        {
            try
            {
                var endereco = await _applicationService.ObterEnderecoPorCepAsync(cep);

                if (endereco is not null)
                    return Ok(endereco);

                return NotFound("Dados do endereço não encontrados para o CEP informado.");
            }
            catch (Exception ex) 
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro ao obter dados do endereço: {ex.Message}");
            }
        }
    }
}

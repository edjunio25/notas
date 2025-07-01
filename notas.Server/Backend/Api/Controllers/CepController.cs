using Microsoft.AspNetCore.Mvc;
using notas.Server.Backend.Domain.Interfaces;
using System.Threading.Tasks;
using notas.Server.Backend.Domain.ValueObjects;

namespace notas.Server.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CepController : ControllerBase
    {
        private readonly ICepService _cepService;

        public CepController(ICepService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> ObterEndereco(string cep)
        {
            try
            {
                var endereco = await _cepService.ObterEnderecoPorCepAsync(cep);

                Console.WriteLine($"Endereço retornado: {endereco?.logradouro}");

                if (endereco == null)
                    return NotFound("CEP inválido ou não encontrado.");

                return Ok(endereco);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao consultar CEP: {ex.Message}");
            }
        }
    }
}

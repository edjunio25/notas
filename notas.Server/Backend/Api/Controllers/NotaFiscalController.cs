using Microsoft.AspNetCore.Mvc;
using notas.Server.Backend.Application.Services;


namespace notas.Server.Backend.Api.Controllers
{
    [ApiController]
    [Route("Backend/Application/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly NotaFiscalService _service;

        public NotaFiscalController(NotaFiscalService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CriarNotaFiscal([FromBody] CriarNotaFiscalDto dto)
        {
            var notaFiscal = await _service.CriarNotaFiscalAsync(dto);
            return notaFiscal == null ? BadRequest("Erro ao criar nota fiscal") : Ok(notaFiscal);
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            var endereco = await _service.MontarEnderecoComCep(cep, "", "");
            return endereco == null ? NotFound("CEP inválido") : Ok(endereco);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using notas.Server.Backend.Application.Interfaces;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Infrastructure.Dto;
using System.Threading.Tasks;

namespace notas.Server.Backend.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly INotaFiscalService _service;

        public NotaFiscalController(INotaFiscalService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarNotaFiscalDto dto)
        {
            var nota = await _service.CriarNotaFiscalAsync(dto);
            return nota == null
                ? BadRequest("Erro ao criar nota fiscal ou empresas não encontradas.")
                : Ok(nota);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var notas = await _service.ListarNotasAsync();
            return Ok(notas);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var sucesso = await _service.ExcluirNotaAsync(id);
            return sucesso ? NoContent() : NotFound();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Infrastructure.Dto;
using System.Threading.Tasks;

namespace notas.Server.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _service;

        public EmpresaController(EmpresaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarEmpresaDto dto)
        {
            var empresa = await _service.CriarEmpresaAsync(dto);
            return empresa == null
                ? Conflict("Já existe uma empresa com este CNPJ.")
                : Ok(empresa);
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var empresas = await _service.ListarEmpresasAsync();
            return Ok(empresas);
        }
    }
}

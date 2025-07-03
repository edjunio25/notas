using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using notas.Server.Backend.Infrastructure.Dto;
using notas.Tests.Integration;
using Xunit;

namespace notas.Tests.Integration
{
    public class NotaFiscalIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public NotaFiscalIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ListarNotas_DeveRetornarStatusOk()
        {
            var response = await _client.GetAsync("/api/NotaFiscal");
            response.EnsureSuccessStatusCode();

            var notas = await response.Content.ReadFromJsonAsync<List<NotaFiscalResponseDto>>();

            Assert.NotNull(notas);
        }

        [Fact]
        public async Task CriarNotaFiscal_DeveRetornarNotaCriada()
        {
            var dto = new CriarNotaFiscalDto
            {
                EmpresaOrigemIdEmpresa = 1, 
                EmpresaDestinoIdEmpresa = 1, 
                NumeroNota = "NF123456789",
                DataEmissao = DateTime.UtcNow,
                ValorTotal = 1000.50m
            };

            var response = await _client.PostAsJsonAsync("/api/NotaFiscal", dto);

            response.EnsureSuccessStatusCode();

            var notaCriada = await response.Content.ReadFromJsonAsync<NotaFiscalResponseDto>();

            Assert.NotNull(notaCriada);
            Assert.Equal("NF123456789", notaCriada.NumeroNota);
        }

        [Fact]
        public async Task ExcluirNotaFiscal_DeveRetornarNoContent()
        {
            var dto = new CriarNotaFiscalDto
            {
                EmpresaOrigemIdEmpresa = 1,
                EmpresaDestinoIdEmpresa = 1,
                NumeroNota = "NFEXCLUIR",
                DataEmissao = DateTime.UtcNow,
                ValorTotal = 500.00m
            };

            var criarResponse = await _client.PostAsJsonAsync("/api/NotaFiscal", dto);
            criarResponse.EnsureSuccessStatusCode();

            var notaCriada = await criarResponse.Content.ReadFromJsonAsync<NotaFiscalResponseDto>();

            Assert.NotNull(notaCriada);
            var excluirResponse = await _client.DeleteAsync($"/api/NotaFiscal/{notaCriada.IdNota}");


            Assert.Equal(HttpStatusCode.NoContent, excluirResponse.StatusCode);
        }
    }
}

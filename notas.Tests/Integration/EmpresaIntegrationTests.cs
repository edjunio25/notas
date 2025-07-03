using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace notas.Tests.Integration
{
    public class EmpresaIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EmpresaIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetEmpresas_DeveRetornarListaComSucesso()
        {
            var response = await _client.GetAsync("/api/empresa");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var empresas = await response.Content.ReadFromJsonAsync<List<EmpresaDto>>();

            Assert.NotNull(empresas);
            Assert.True(empresas.Count > 0);
        }

        [Fact]
        public async Task CriarEmpresa_DeveRetornarEmpresaCriada()
        {
            var novaEmpresa = new
            {
                razaoSocial = "Empresa de Teste",
                nomeFantasia = "Fantasia Teste",
                cnpj = "12345678000199",
                endereco = new
                {
                    logradouro = "Rua Teste",
                    numero = "123",
                    complemento = "Sala 1",
                    bairro = "Centro",
                    cidade = "Testópolis",
                    uf = 10, 
                    cep = "30000000"
                }
            };

            var response = await _client.PostAsJsonAsync("/api/Empresa", novaEmpresa);

            response.EnsureSuccessStatusCode();
            var empresaCriada = await response.Content.ReadFromJsonAsync<EmpresaDto>();
            Assert.Equal("Empresa de Teste", empresaCriada!.RazaoSocial);
            Assert.Equal("12345678000199", empresaCriada.Cnpj);
        }

        [Fact]
        public async Task DesativarEmpresa_DeveRetornarNoContent()
        {
            var empresaDto = new
            {
                razaoSocial = "Empresa Ativa",
                nomeFantasia = "Ativa",
                cnpj = "98765432000100",
                endereco = new
                {
                    logradouro = "Av. Desativar",
                    numero = "456",
                    complemento = "",
                    bairro = "Bairro",
                    cidade = "Cidade",
                    uf = 25, // SP
                    cep = "12345000"
                }
            };

            var postResponse = await _client.PostAsJsonAsync("/api/Empresa", empresaDto);
            postResponse.EnsureSuccessStatusCode();
            var criada = await postResponse.Content.ReadFromJsonAsync<EmpresaDto>();

            var desativarResponse = await _client.PutAsync($"/api/Empresa/{ criada!.IdEmpresa}/desativar", null);

            Assert.Equal(HttpStatusCode.NoContent, desativarResponse.StatusCode);
        }

    }
}

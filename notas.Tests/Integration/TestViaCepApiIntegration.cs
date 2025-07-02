using Microsoft.VisualStudio.TestPlatform.TestHost;
using notas.Server.Backend.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace notas.Tests.Integration
{
    public class TestViaCepApiIntegration : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public TestViaCepApiIntegration(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ObterEndereco_DeveRetornarEndereco_QuandoCepValido()
        {
            Console.WriteLine("Iniciando teste de integração para obter endereço por CEP");
            var response = await _client.GetAsync("/api/cep/30140071");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var endereco = JsonSerializer.Deserialize<Endereco>(content);

            Assert.NotNull(endereco); 
            Assert.Equal("MG", endereco.uf.ToString());
        }

        [Fact]
        public async Task ObterEndereco_DeveRetornarNotFound_QuandoCepInvalido()
        {
            Console.WriteLine("Iniciando teste de integração para CEP inválido");
            var response = await _client.GetAsync("/api/cep/00000000");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Infrastructure.Services;
using Xunit;

namespace notas.Tests
{
    public class TestViaCepService
    {
        private class StubHttpMessageHandler : HttpMessageHandler
        {
            private readonly HttpResponseMessage _response;
            public StubHttpMessageHandler(HttpResponseMessage response)
            {
                _response = response;
            }
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_response);
            }
        }

        private ViaCepService CreateService(HttpResponseMessage response)
        {
            var handler = new StubHttpMessageHandler(response);
            var client = new HttpClient(handler);
            return new ViaCepService(client);
        }

        [Fact]
        public async Task ObterEnderecoPorCepAsync_DeveRetornarEndereco_QuandoJsonValido()
        {
            string json = "{\"Logradouro\":\"Rua A\",\"Bairro\":\"Centro\",\"Localidade\":\"Cidade\",\"Uf\":\"MG\",\"Cep\":\"30110-020\"}";
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            };
            var service = CreateService(response);
            var endereco = await service.ObterEnderecoPorCepAsync("30110-020");
            Assert.NotNull(endereco);
            Assert.Equal("30110020", endereco!.cep);
            Assert.Equal(UF.MG, endereco.uf);
        }

        [Fact]
        public async Task ObterEnderecoPorCepAsync_DeveRetornarNull_QuandoNaoEncontrado()
        {
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);
            var service = CreateService(response);
            var endereco = await service.ObterEnderecoPorCepAsync("00000000");
            Assert.Null(endereco);
        }

        [Fact]
        public async Task ObterEnderecoPorCepAsync_DeveRetornarNull_QuandoJsonInvalido()
        {
            string json = "{\"Cep\":\"30110-020\"}"; // faltando UF e outros campos
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json)
            };
            var service = CreateService(response);
            var endereco = await service.ObterEnderecoPorCepAsync("30110-020");
            Assert.Null(endereco);
        }
    }
}

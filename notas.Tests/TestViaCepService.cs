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
            string json = "{\"cep\":\"30110-020\",\"logradouro\":\"Rua A\",\"complemento\":\"\",\"bairro\":\"Centro\",\"localidade\":\"Cidade\",\"uf\":\"MG\"}";
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

        private class ThrowingHttpMessageHandler : HttpMessageHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                throw new HttpRequestException("Network failure");
            }
        }

        [Fact]
        public async Task ObterEnderecoPorCepAsync_DeveRetornarNull_QuandoHttpRequestFalhar()
        {
            var client = new HttpClient(new ThrowingHttpMessageHandler());
            var service = new ViaCepService(client);
            var endereco = await service.ObterEnderecoPorCepAsync("30110-020");
            Assert.Null(endereco);
        }
    }
}

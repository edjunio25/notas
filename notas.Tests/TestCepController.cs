using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using notas.Server.Backend.Api.Controllers;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace notas.Tests
{
    public class TestCepController
    {
        [Fact]
        public async Task ObterEnderecoDeveRetornarOkQuandoEncontrado()
        {
            var serviceMock = new Mock<ICepService>();
            var endereco = new Endereco("Rua A", "", "123", "Centro", "Cidade", UF.MG, "30110-020");
            serviceMock.Setup(s => s.ObterEnderecoPorCepAsync("30110-020"))
                       .ReturnsAsync(endereco);

            var controller = new CepController(serviceMock.Object);

            var resultado = await controller.ObterEndereco("30110-020");

            var ok = Assert.IsType<OkObjectResult>(resultado);
            Assert.Equal(endereco, ok.Value);
        }

        [Fact]
        public async Task ObterEnderecoDeveRetornarNotFoundQuandoNaoEncontrado()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(s => s.ObterEnderecoPorCepAsync("00000000"))
                       .ReturnsAsync((Endereco?)null);

            var controller = new CepController(serviceMock.Object);

            var resultado = await controller.ObterEndereco("00000000");

            Assert.IsType<NotFoundObjectResult>(resultado);
        }

        [Fact]
        public async Task ObterEnderecoDeveRetornarStatus500QuandoExcecao()
        {
            var serviceMock = new Mock<ICepService>();
            serviceMock.Setup(s => s.ObterEnderecoPorCepAsync(It.IsAny<string>()))
                       .ThrowsAsync(new Exception("Falha"));

            var controller = new CepController(serviceMock.Object);

            var resultado = await controller.ObterEndereco("30110-020");

            var status = Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, status.StatusCode);
        }
    }
}
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using notas.Server.Backend.Api.Controllers;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Infrastructure.Dto;
using notas.Server.Backend.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using notas.Server.Backend.Application.Interfaces;

namespace notas.Tests
{
    public class TestNotaFiscalController
    {
        [Fact]
        public async Task CriarDeveRetornarOkSeNotaCriada()
        {
            var mockService = new Mock<INotaFiscalService>();
            var dto = new CriarNotaFiscalDto
            {
                EmpresaOrigemIdEmpresa = 1,
                EmpresaDestinoIdEmpresa = 2,
                NumeroNota = "12345",
                Serie = "1",
                ChaveAcesso = "ABC123",
                TipoNota = TipoNota.NFE,
                ValorTotal = 100,
                DataEmissao = System.DateTime.UtcNow,
                DataPostagem = System.DateTime.UtcNow,
                Descricao = "Teste"
            };

            var notaMock = new NotaFiscal(
                new Empresa("A", "B", "12345678000199", new Endereco()),
                new Empresa("C", "D", "98765432000188", new Endereco()),
                "12345",
                "ABC123",
                "1",
                TipoNota.NFE,
                100,
                dto.DataEmissao,
                dto.DataPostagem,
                "Teste"
            );

            mockService.Setup(s => s.CriarNotaFiscalAsync(dto))
                       .ReturnsAsync(notaMock);

            var controller = new NotaFiscalController(mockService.Object);

            var result = await controller.Criar(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(notaMock, ok.Value);
        }

        [Fact]
        public async Task CriarDeveRetornarBadRequestSeNotaNaoCriada()
        {
            var mockService = new Mock<INotaFiscalService>();
            var dto = new CriarNotaFiscalDto();

            mockService.Setup(s => s.CriarNotaFiscalAsync(dto))
                       .ReturnsAsync((NotaFiscal)null!);

            var controller = new NotaFiscalController(mockService.Object);

            var result = await controller.Criar(dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ListarDeveRetornarOkComListaDeNotas()
        {
            var mockService = new Mock<INotaFiscalService>();
            var lista = new List<NotaFiscal>();
            mockService.Setup(s => s.ListarNotasAsync())
                       .ReturnsAsync(lista);

            var controller = new NotaFiscalController(mockService.Object);

            var result = await controller.Listar();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(lista, ok.Value);
        }

        [Fact]
        public async Task ExcluirDeveRetornarNoContentQuandoSucesso()
        {
            var mockService = new Mock<INotaFiscalService>();
            mockService.Setup(s => s.ExcluirNotaAsync(1)).ReturnsAsync(true);

            var controller = new NotaFiscalController(mockService.Object);

            var result = await controller.Excluir(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task ExcluirDeveRetornarNotFoundQuandoFalha()
        {
            var mockService = new Mock<INotaFiscalService>();
            mockService.Setup(s => s.ExcluirNotaAsync(1)).ReturnsAsync(false);

            var controller = new NotaFiscalController(mockService.Object);

            var result = await controller.Excluir(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}

using Moq;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Infrastructure.Dto;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notas.Tests
{
    public class TestNotaFiscalService
    {
        [Fact]
        public async Task CriarNotaFiscalAsync_DeveRetornarNull_SeEmpresaNaoExiste()
        {
            // Arrange
            var notaRepoMock = new Mock<INotaFiscalRepository>();
            var empresaRepoMock = new Mock<IEmpresaRepository>();

            empresaRepoMock.Setup(r => r.BuscarPorIdAsync(It.IsAny<int>())).ReturnsAsync((notas.Server.Backend.Domain.Entities.Empresa?)null);

            var service = new NotaFiscalService(notaRepoMock.Object, empresaRepoMock.Object);

            var dto = new CriarNotaFiscalDto
            {
                EmpresaOrigemIdEmpresa = 1,
                EmpresaDestinoIdEmpresa = 2,
                NumeroNota = "123",
                ChaveAcesso = "abc",
                Serie = "A1",
                TipoNota = notas.Server.Backend.Domain.Enums.TipoNota.NFS,
                ValorTotal = 100,
                DataEmissao = System.DateTime.Now,
                DataPostagem = System.DateTime.Now,
                Descricao = "Teste"
            };

            // Act
            var resultado = await service.CriarNotaFiscalAsync(dto);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task CriarNotaFiscalAsync_DeveRetornarNotaFiscal_QuandoEmpresasExistem()
        {
            // Arrange
            var notaRepoMock = new Mock<INotaFiscalRepository>();
            var empresaRepoMock = new Mock<IEmpresaRepository>();

            var endereco = new Endereco("Rua Teste", "", "123", "Centro", "Cidade", UF.MG, "30110-020");
            var empresaOrigem = new Empresa("Origem", "Origem", "12345678910111", endereco);
            var empresaDestino = new Empresa("Destino", "Destino", "10987654321011", endereco);

            empresaRepoMock.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(empresaOrigem);
            empresaRepoMock.Setup(r => r.BuscarPorIdAsync(2)).ReturnsAsync(empresaDestino);

            var service = new NotaFiscalService(notaRepoMock.Object, empresaRepoMock.Object);

            var dto = new CriarNotaFiscalDto
            {
                EmpresaOrigemIdEmpresa = 1,
                EmpresaDestinoIdEmpresa = 2,
                NumeroNota = "123",
                ChaveAcesso = "abc",
                Serie = "A1",
                TipoNota = TipoNota.NFS,
                ValorTotal = 100,
                DataEmissao = DateTime.Now,
                DataPostagem = DateTime.Now,
                Descricao = "Teste"
            };

            // Act
            var resultado = await service.CriarNotaFiscalAsync(dto);

            // Assert
            Assert.NotNull(resultado);
            notaRepoMock.Verify(r => r.SalvarAsync(It.IsAny<NotaFiscal>()), Times.Once);
        }

        [Fact]
        public async Task CriarNotaFiscalAsync_DeveLancarExcecao_QuandoValorInvalido()
        {
            var notaRepoMock = new Mock<INotaFiscalRepository>();
            var empresaRepoMock = new Mock<IEmpresaRepository>();

            var endereco = new Endereco("Rua Teste", "", "123", "Centro", "Cidade", UF.MG, "30110-020");
            var empresaOrigem = new Empresa("Origem", "Origem", "12345678910111", endereco);
            var empresaDestino = new Empresa("Destino", "Destino", "10987654321011", endereco);
            empresaRepoMock.Setup(r => r.BuscarPorIdAsync(1)).ReturnsAsync(empresaOrigem);
            empresaRepoMock.Setup(r => r.BuscarPorIdAsync(2)).ReturnsAsync(empresaDestino);

            var service = new NotaFiscalService(notaRepoMock.Object, empresaRepoMock.Object);
            var dto = new CriarNotaFiscalDto
            {
                EmpresaOrigemIdEmpresa = 1,
                EmpresaDestinoIdEmpresa = 2,
                NumeroNota = "123",
                ChaveAcesso = "abc",
                Serie = "A1",
                TipoNota = TipoNota.NFS,
                ValorTotal = 0,
                DataEmissao = DateTime.Now,
                DataPostagem = DateTime.Now,
                Descricao = "Teste"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CriarNotaFiscalAsync(dto));
            notaRepoMock.Verify(r => r.SalvarAsync(It.IsAny<NotaFiscal>()), Times.Never);
        }

        [Fact]
        public async Task ListarNotasAsync_DeveRetornarTodasNotas()
        {
            var notas = new List<NotaFiscal>
            {
                new NotaFiscal(new Empresa("A", "A", "12345678910111", new Endereco("a", "", "1", "b", "c", UF.MG, "30110-020")),
                               new Empresa("B", "B", "10987654321011", new Endereco("a", "", "1", "b", "c", UF.MG, "30110-020")),
                               "1", "c", "s", TipoNota.NFE, 10, DateTime.Now, DateTime.Now, ""),
                new NotaFiscal(new Empresa("A2", "A2", "12345678910112", new Endereco("a", "", "1", "b", "c", UF.MG, "30110-020")),
                               new Empresa("B2", "B2", "10987654321012", new Endereco("a", "", "1", "b", "c", UF.MG, "30110-020")),
                               "2", "c", "s", TipoNota.NFE, 20, DateTime.Now, DateTime.Now, "")
            };
            var notaRepoMock2 = new Mock<INotaFiscalRepository>();
            notaRepoMock2.Setup(r => r.ListarAsync()).ReturnsAsync(notas);
            var service2 = new NotaFiscalService(notaRepoMock2.Object, new Mock<IEmpresaRepository>().Object);
            var resultado = await service2.ListarNotasAsync();
            Assert.Equal(2, resultado.Count());
            notaRepoMock2.Verify(r => r.ListarAsync(), Times.Once);
        }
    }
}
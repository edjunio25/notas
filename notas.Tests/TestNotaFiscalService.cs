using Moq;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Infrastructure.Dto;
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
    }
}

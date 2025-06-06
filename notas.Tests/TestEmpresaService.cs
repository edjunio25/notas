using Moq;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Infrastructure.Dto;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace notas.Tests
{
    public class TestEmpresaService
    {
        private Endereco CriarEnderecoDummy()
        {
            return new Endereco(
                "Rua Teste",
                "123",
                "Complemento",
                "Bairro",
                "Cidade",
                UF.MG,
                "00000000"
            );
        }

        [Fact]
        public async Task CriarEmpresaAsync_DeveRetornarNull_SeEmpresaJaExiste()
        {
            // Arrange
            var repoMock = new Mock<IEmpresaRepository>();
            repoMock.Setup(r => r.BuscarPorCnpjAsync(It.IsAny<string>()))
                .ReturnsAsync(new Empresa("Teste", "Fantasia", "12345678910111", CriarEnderecoDummy()));

            var service = new EmpresaService(repoMock.Object);
            var dto = new CriarEmpresaDto
            {
                RazaoSocial = "Teste",
                NomeFantasia = "Fantasia",
                Cnpj = "12345678910111",
                Endereco = CriarEnderecoDummy()
            };

            // Act
            var resultado = await service.CriarEmpresaAsync(dto);

            // Assert
            Assert.Null(resultado);
            repoMock.Verify(r => r.SalvarAsync(It.IsAny<Empresa>()), Times.Never);
        }

        [Fact]
        public async Task CriarEmpresaAsync_DeveSalvarEmpresa_QuandoNaoExiste()
        {
            // Arrange
            var repoMock = new Mock<IEmpresaRepository>();
            repoMock.Setup(r => r.BuscarPorCnpjAsync(It.IsAny<string>()))
                .ReturnsAsync((Empresa?)null);

            var service = new EmpresaService(repoMock.Object);
            var dto = new CriarEmpresaDto
            {
                RazaoSocial = "Teste",
                NomeFantasia = "Fantasia",
                Cnpj = "12345678910111",
                Endereco = CriarEnderecoDummy()
            };

            // Act
            var resultado = await service.CriarEmpresaAsync(dto);

            // Assert
            Assert.NotNull(resultado);
            repoMock.Verify(r => r.SalvarAsync(It.IsAny<Empresa>()), Times.Once);
            Assert.Equal("Teste", resultado!.RazaoSocial);
        }

        [Fact]
        public async Task CriarEmpresaAsync_DeveLancarExcecao_QuandoDadosInvalidos()
        {
            var repoMock = new Mock<IEmpresaRepository>();
            repoMock.Setup(r => r.BuscarPorCnpjAsync(It.IsAny<string>()))
                .ReturnsAsync((Empresa?)null);

            var service = new EmpresaService(repoMock.Object);
            var dto = new CriarEmpresaDto
            {
                RazaoSocial = "Teste",
                NomeFantasia = "Fantasia",
                Cnpj = "123", // CNPJ invalido
                Endereco = CriarEnderecoDummy()
            };

            await Assert.ThrowsAsync<ArgumentException>(() => service.CriarEmpresaAsync(dto));
            repoMock.Verify(r => r.SalvarAsync(It.IsAny<Empresa>()), Times.Never);
        }

        [Fact]
        public async Task ListarEmpresasAsync_DeveRetornarTodasEmpresas()
        {
            var empresas = new List<Empresa>
            {
                new Empresa("A", "A", "11111111111111", CriarEnderecoDummy()),
                new Empresa("B", "B", "22222222222222", CriarEnderecoDummy())
            };

            var repoMock = new Mock<IEmpresaRepository>();
            repoMock.Setup(r => r.ListarTodasAsync()).ReturnsAsync(empresas);

            var service = new EmpresaService(repoMock.Object);
            var resultado = await service.ListarEmpresasAsync();

            Assert.Equal(2, resultado.Count());
            repoMock.Verify(r => r.ListarTodasAsync(), Times.Once);
        }

        [Fact]
        public async Task ListarEmpresasAsync_DeveRetornarListaVazia_QuandoNaoExistemEmpresas()
        {
            var repoMock = new Mock<IEmpresaRepository>();
            repoMock.Setup(r => r.ListarTodasAsync()).ReturnsAsync(new List<Empresa>());

            var service = new EmpresaService(repoMock.Object);
            var resultado = await service.ListarEmpresasAsync();

            Assert.Empty(resultado);
            repoMock.Verify(r => r.ListarTodasAsync(), Times.Once);
        }
    }
}
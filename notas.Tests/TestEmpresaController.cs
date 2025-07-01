using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using notas.Server.Backend.API.Controllers;
using notas.Server.Backend.Application.Services;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Infrastructure.Dto;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using notas.Server.Backend.Application.Interfaces;

namespace notas.Tests
{
    public class TestEmpresaController
    {
        [Fact]
        public async Task CriarDeveRetornarOkSeEmpresaCriada()
        {
            var mockService = new Mock<IEmpresaService>();
            var dto = new CriarEmpresaDto
            {
                RazaoSocial = "Empresa Teste",
                NomeFantasia = "Fantasia",
                Cnpj = "12345678000199",
                Endereco = new Endereco("Rua A", "", "123", "Centro", "Cidade", UF.MG, "30110-020")
            };

            var empresaMock = new Empresa(dto.RazaoSocial, dto.NomeFantasia, dto.Cnpj, dto.Endereco);

            mockService.Setup(s => s.CriarEmpresaAsync(dto))
                       .ReturnsAsync(empresaMock);

            var controller = new EmpresaController(mockService.Object);

            var result = await controller.Criar(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(empresaMock, ok.Value);
        }

        [Fact]
        public async Task CriarDeveRetornarConflictSeEmpresaJaExistir()
        {
            var mockService = new Mock<IEmpresaService>();
            var dto = new CriarEmpresaDto();

            mockService.Setup(s => s.CriarEmpresaAsync(dto))
                       .ReturnsAsync((Empresa)null!);

            var controller = new EmpresaController(mockService.Object);

            var result = await controller.Criar(dto);

            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task ListarDeveRetornarOkComListaDeEmpresas()
        {
            var mockService = new Mock<IEmpresaService>();
            var lista = new List<Empresa>();
            mockService.Setup(s => s.ListarEmpresasAsync())
                       .ReturnsAsync(lista);

            var controller = new EmpresaController(mockService.Object);

            var result = await controller.Listar();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(lista, ok.Value);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Infrastructure.Data;
using System.Threading.Tasks;
using Xunit;

namespace notas.Tests
{
    public class TestEmpresaRepository
    {
        private AppDbContext CriarContextoEmMemoria()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TesteEmpresaDB")
                .Options;
            // Essa funcionalidade InMemoryDatabase simula um banco de dados em memória para testes, assim não testamos o banco real, o que pode ser feito em um teste de integração separado.
            return new AppDbContext(options);
        }

        private Empresa CriarEmpresaExemplo()
        {
            return new Empresa(
                "Empresa Teste",
                "Fantasia",
                "12345678901234",
                new Endereco("Rua A", "", "123", "Centro", "Belo Horizonte", UF.MG, "30110020")
            );
        }

        [Fact]
        public async Task DeveSalvarEBuscarEmpresaPorCnpj()
        {
            var contexto = CriarContextoEmMemoria();
            var repositorio = new EmpresaRepository(contexto);
            var empresa = CriarEmpresaExemplo();

            await repositorio.SalvarAsync(empresa);
            var encontrada = await repositorio.BuscarPorCnpjAsync("12345678901234");

            Assert.NotNull(encontrada);
            Assert.Equal("Empresa Teste", encontrada!.RazaoSocial);
        }

        [Fact]
        public async Task DeveListarTodasEmpresas()
        {
            var contexto = CriarContextoEmMemoria();
            var repositorio = new EmpresaRepository(contexto);

            await repositorio.SalvarAsync(CriarEmpresaExemplo());
            await repositorio.SalvarAsync(CriarEmpresaExemplo());

            var todas = await repositorio.ListarTodasAsync();
            Assert.Equal(2, System.Linq.Enumerable.Count(todas));
        }
    }
}

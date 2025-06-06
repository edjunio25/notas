using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Infrastructure.Data;
using System.Threading.Tasks;
using Xunit;

public class TestNotaFiscalRepository
{
    private AppDbContext CriarContextoEmMemoria()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TesteNotaDB")
            .Options;
        return new AppDbContext(options);
    }

    private NotaFiscal CriarNotaValida()
    {
        var origem = new Empresa("Origem", "Origem LTDA", "11111111111111",
            new Endereco("Rua A", "", "1", "Centro", "BH", UF.MG, "30110000"));
        var destino = new Empresa("Destino", "Destino LTDA", "22222222222222",
            new Endereco("Rua B", "", "2", "Savassi", "BH", UF.MG, "30120000"));

        return new NotaFiscal(origem, destino, "NF123", "CHV123", "1", TipoNota.NFE, 500m, DateTime.Today, DateTime.Today, "Descrição teste");
    }

    [Fact]
    public async Task DeveSalvarENotarNotaFiscal()
    {
        var contexto = CriarContextoEmMemoria();
        var repositorio = new NotaFiscalRepository(contexto);
        var nota = CriarNotaValida();

        await repositorio.SalvarAsync(nota);
        var lista = await repositorio.ListarAsync();

        Assert.Single(lista);
        Assert.Equal("NF123", lista.First().NumeroNota);
    }
}

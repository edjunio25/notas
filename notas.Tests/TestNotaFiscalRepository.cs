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

    private NotaFiscal CriarNotaComNumero(string numero)
    {
        var origem = new Empresa("Origem", "Origem LTDA", "11111111111111",
            new Endereco("Rua A", "", "1", "Centro", "BH", UF.MG, "30110000"));
        var destino = new Empresa("Destino", "Destino LTDA", "22222222222222",
            new Endereco("Rua B", "", "2", "Savassi", "BH", UF.MG, "30120000"));

        return new NotaFiscal(origem, destino, numero, "CHV" + numero, "1", TipoNota.NFE,
            500m, DateTime.Today, DateTime.Today, "Descrição teste");
    }

    [Fact]
    public async Task BuscarPorNumeroAsync_DeveRetornarNotaCorreta()
    {
        var contexto = CriarContextoEmMemoria();
        var repositorio = new NotaFiscalRepository(contexto);

        var nota1 = CriarNotaComNumero("NF1");
        var nota2 = CriarNotaComNumero("NF2");

        await repositorio.SalvarAsync(nota1);
        await repositorio.SalvarAsync(nota2);

        var encontrada = await repositorio.BuscarPorNumeroAsync("NF2");

        Assert.NotNull(encontrada);
        Assert.Equal("NF2", encontrada!.NumeroNota);
    }

    [Fact]
    public async Task BuscarPorIdAsync_DeveRetornarComEmpresas()
    {
        var contexto = CriarContextoEmMemoria();
        var repositorio = new NotaFiscalRepository(contexto);
        var nota = CriarNotaComNumero("NFIncl");

        await repositorio.SalvarAsync(nota);
        var id = nota.IdNota;

        contexto.Entry(nota).State = EntityState.Detached;
        var encontrada = await repositorio.BuscarPorIdAsync(id);

        Assert.NotNull(encontrada);
        Assert.NotNull(encontrada!.EmpresaOrigem);
        Assert.NotNull(encontrada!.EmpresaDestino);
        Assert.Equal("Origem", encontrada.EmpresaOrigem.RazaoSocial);
        Assert.Equal("Destino", encontrada.EmpresaDestino.RazaoSocial);
    }

    [Fact]
    public async Task ExcluirAsync_DeveRemoverNota()
    {
        var contexto = CriarContextoEmMemoria();
        var repositorio = new NotaFiscalRepository(contexto);
        var nota = CriarNotaComNumero("NFDEL");

        await repositorio.SalvarAsync(nota);
        await repositorio.ExcluirAsync(nota);

        var resultado = await repositorio.BuscarPorNumeroAsync("NFDEL");
        Assert.Null(resultado);
    }
}

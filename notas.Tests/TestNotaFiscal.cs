using System;
using System.Globalization;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;
using Xunit;

namespace notas.Tests
{
    public class TestNotaFiscal
    {
        private Endereco CriarEnderecoDummy()
        {
            return new Endereco(
                "Rua A",
                "Apto 1",
                "123",
                "Centro",
                "Cidade",
                UF.MG,
                "30110-020"
            );
        }

        private Empresa CriarEmpresaDummy(string razao)
        {
            return new Empresa(razao, razao, "12345678910111", CriarEnderecoDummy());
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoEmpresaOrigemNula()
        {
            var destino = CriarEmpresaDummy("Destino");
            Assert.Throws<ArgumentNullException>(() =>
                new NotaFiscal(null!, destino, "1", "", "", TipoNota.NFS, 10,
                    DateTime.Now, DateTime.Now, "")
            );
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoEmpresaDestinoNula()
        {
            var origem = CriarEmpresaDummy("Origem");
            Assert.Throws<ArgumentNullException>(() =>
                new NotaFiscal(origem, null!, "1", "", "", TipoNota.NFS, 10,
                    DateTime.Now, DateTime.Now, "")
            );
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoNumeroNotaInvalido()
        {
            var origem = CriarEmpresaDummy("Origem");
            var destino = CriarEmpresaDummy("Destino");
            Assert.Throws<ArgumentException>(() =>
                new NotaFiscal(origem, destino, "", "", "", TipoNota.NFS, 10,
                    DateTime.Now, DateTime.Now, "")
            );
        }

        [Fact]
        public void Construtor_DeveLancarExcecao_QuandoValorTotalMenorOuIgualZero()
        {
            var origem = CriarEmpresaDummy("Origem");
            var destino = CriarEmpresaDummy("Destino");
            Assert.Throws<ArgumentException>(() =>
                new NotaFiscal(origem, destino, "1", "", "", TipoNota.NFS, 0,
                    DateTime.Now, DateTime.Now, "")
            );
        }

        [Fact]
        public void ToString_DeveRetornarRepresentacaoCorreta()
        {
            var origem = CriarEmpresaDummy("Origem");
            var destino = CriarEmpresaDummy("Destino");
            var emissao = new DateTime(2024, 1, 1);
            var nota = new NotaFiscal(origem, destino, "1", "CHAVE", "A1", TipoNota.NFS,
                100m, emissao, emissao, "Teste");

            var current = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                var expected = $"{TipoNota.NFS} 1 - {100m.ToString("C")} ({emissao:dd/MM/yyyy})";
                Assert.Equal(expected, nota.ToString());
            }
            finally
            {
                CultureInfo.CurrentCulture = current;
            }
        }
    }
}
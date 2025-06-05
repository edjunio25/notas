using System;
using Xunit;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Domain.Enums;

namespace notas.Tests
{
    public class TestEmpresa
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
        public void TestCriarEmpresaLevantaExcecaoAoNaoInformarRazaoSocial()
        {
            var endereco = CriarEnderecoDummy();

            Assert.Throws<ArgumentException>(() =>
                new Empresa("", "Teste Fantasia", "00000000000000", endereco)
            );
        }

        [Fact]
        public void TestCriarEmpresaLevantaExcecaoAoInserirCnpjMenorQue14Caracteres()
        {
            var endereco = CriarEnderecoDummy();

            Assert.Throws<ArgumentException>(() =>
                new Empresa("Teste", "Teste Fantasia", "123456789", endereco)
            );
        }

        [Fact]
        public void TestCriarEmpresaLevantaExcecaoAoInserirCnpjMaiorQue14Caracteres()
        {
            var endereco = CriarEnderecoDummy();

            Assert.Throws<ArgumentException>(() =>
                new Empresa("Teste", "Teste Fantasia", "123456789101213", endereco)
            );
        }

    }
}

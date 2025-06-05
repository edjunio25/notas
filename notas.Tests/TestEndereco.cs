using System;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;
using Xunit;

namespace notas.Tests
{
    public class EnderecoTests
    {
        [Fact]
        public void CriarEnderecoComCepValidoDeveCriarComSucesso()
        {
            var endereco = new Endereco("Rua A", "Apto 1", "123", "Centro", "Cidade", UF.MG, "30110-020");

            Assert.Equal("30110020", endereco.cep);
        }

        [Fact]
        public void CriarEnderecoComCepVazioDeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() =>
                new Endereco("Rua A", "", "123", "Centro", "Cidade", UF.MG, "")
            );
        }

        [Fact]
        public void CriarEnderecoComCepInvalidoDeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() =>
                new Endereco("Rua A", "", "123", "Centro", "Cidade", UF.MG, "ABC-1234")
            );
        }

        [Fact]
        public void RemoverCaracteresEspeciaisDeveRetornarApenasNumeros()
        {
            var metodo = typeof(Endereco).GetMethod("RemoverCaracteresEspeciais", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            string result = (string)metodo.Invoke(null, new object[] { "30.110-020" });

            Assert.Equal("30110020", result);
        }

        [Theory]
        [InlineData("30110020", true)]
        [InlineData("1234567", false)]
        [InlineData("ABC12345", false)]
        public void ValidarCep_DeveRetornarCorreto(string cep, bool esperadoValido)
        {
            var valido = Endereco.ValidarCep(cep);
            Assert.Equal(esperadoValido, valido);
        }

    }
}

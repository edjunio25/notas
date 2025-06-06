using System;
using Xunit;
using System.Threading;
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

        [Fact]
        public void TestCriarEmpresaComDadosValidos()
        {
            var endereco = CriarEnderecoDummy();
            var empresa = new Empresa("Teste", "Teste Fantasia", "12345678910111", endereco);
            Assert.NotNull(empresa);
            Assert.Equal("Teste", empresa.RazaoSocial);
            Assert.Equal("Teste Fantasia", empresa.NomeFantasia);
            Assert.Equal("12345678910111", empresa.Cnpj);
            Assert.Equal(endereco, empresa.EnderecoEmpresa);
        }

        [Fact]
        public void TestAtualizarDadosEmpresaComDadosValidos()
        {
            var endereco = CriarEnderecoDummy();
            var empresa = new Empresa("Teste", "Teste Fantasia", "12345678910111", endereco);
            var novoEndereco = CriarEnderecoDummy();
            empresa.AtualizarDados("Nova Razao Social", "Novo Nome Fantasia", novoEndereco);
            Assert.Equal("Nova Razao Social", empresa.RazaoSocial);
            Assert.Equal("Novo Nome Fantasia", empresa.NomeFantasia);
            Assert.Equal(novoEndereco, empresa.EnderecoEmpresa);
        }

        [Fact]
        public void TestAlternarAtivacaoEmpresa()
        {
            var endereco = CriarEnderecoDummy();
            var empresa = new Empresa("Teste", "Teste Fantasia", "12345678910111", endereco);

            Assert.Equal(1, empresa.IsAtiva);

            empresa.AlternarAtivacao();
            Assert.Equal(0, empresa.IsAtiva);

            empresa.AlternarAtivacao();
            Assert.Equal(1, empresa.IsAtiva);
        }

        [Fact]
        public void TestToStringEmpresaRetornaAStringCorreta()
        {
            var endereco = CriarEnderecoDummy();
            var empresa = new Empresa("Teste", "Teste Fantasia", "12345678910111", endereco);
            Assert.Equal("Teste (12345678910111)", empresa.ToString());
        }

        [Fact]
        public void AlternarAtivacao_DeveAtualizarDataUltimaAtualizacao()
        {
            var endereco = CriarEnderecoDummy();
            var empresa = new Empresa("Teste", "Teste Fantasia", "12345678910111", endereco);
            var antes = empresa.DataUltimaAtualizacao;
            Thread.Sleep(1);
            empresa.AlternarAtivacao();
            Assert.True(empresa.DataUltimaAtualizacao > antes);
        }

        [Fact]
        public void AtualizarDados_DeveAtualizarDataUltimaAtualizacao()
        {
            var endereco = CriarEnderecoDummy();
            var empresa = new Empresa("Teste", "Teste Fantasia", "12345678910111", endereco);
            var antes = empresa.DataUltimaAtualizacao;
            Thread.Sleep(1);
            empresa.AtualizarDados("Nova Razao Social", "Novo Nome Fantasia", CriarEnderecoDummy());
            Assert.True(empresa.DataUltimaAtualizacao > antes);
        }

        //Este teste estava flaky, pois a comparação com DateTime.UtcNow pode falhar dependendo do tempo de execução do teste.
        //Executando na máquina local, o teste passava, mas no CI falhou.
        //Colocamos um range para estabelecer uma tolerancia de tempo.
        //Atualização: colocando um range de 1 segundo rodava corretamente na maquina, porem no actions não. Vamos adicionar um "antes" e "depois" para comparar a diferença de tempo, mantendo a tolerancia de 1 segundo.
        [Fact]
        public void CriarEmpresa_DeveInicializarDatasCorretamente()
        {
            var endereco = CriarEnderecoDummy();
            var antes = DateTime.UtcNow;
            var tolerancia = TimeSpan.FromSeconds(1);

            var empresa = new Empresa("Teste", "Teste Fantasia", "12345678910111", endereco);
            var depois = DateTime.UtcNow;

            Assert.InRange(empresa.DataCriacao, antes - tolerancia, depois + tolerancia);
            Assert.InRange(empresa.DataUltimaAtualizacao, antes - tolerancia, depois + tolerancia);
            Assert.True(Math.Abs((empresa.DataCriacao - empresa.DataUltimaAtualizacao).TotalMilliseconds) < 1000);
        }



    }
}

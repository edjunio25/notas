using System;
using System.Security.Permissions;
using notas.Server.Backend.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;


namespace notas.Server.Backend.Domain.Entities
{
    public class Empresa
    {
        [Key]
        public int IdEmpresa { get; private set; }
        public int IsAtiva { get; private set; } = 1; // 1 = ativa, 0 = inativa
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Cnpj { get; private set; }
        public Endereco EnderecoEmpresa { get; private set; } = new Endereco();
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;
        public DateTime DataUltimaAtualizacao { get; private set; } = DateTime.UtcNow;

        protected Empresa() { }

        public Empresa(string razaoSocialInput, string nomeFantasiaInput, string cnpjInput, Endereco enderecoInput)
        {
            if (string.IsNullOrWhiteSpace(razaoSocialInput))
                throw new ArgumentException("Razão social é obrigatória.");

            if (string.IsNullOrWhiteSpace(cnpjInput) || cnpjInput.Length != 14)
                throw new ArgumentException("CNPJ inválido.");

            RazaoSocial = razaoSocialInput;
            NomeFantasia = nomeFantasiaInput ?? string.Empty;
            Cnpj = cnpjInput;
            EnderecoEmpresa = enderecoInput ?? throw new ArgumentNullException(nameof(enderecoInput));
        }

        public void AtualizarDados(string razaoSocialInput, string nomeFantasiaInput, Endereco enderecoInput)
        {
            if (!string.IsNullOrWhiteSpace(razaoSocialInput))
                RazaoSocial = razaoSocialInput;

            if (!string.IsNullOrWhiteSpace(nomeFantasiaInput))
                NomeFantasia = nomeFantasiaInput;

            if (enderecoInput != null)
                EnderecoEmpresa = enderecoInput;

            DataUltimaAtualizacao = DateTime.UtcNow;
        }

        public void AlternarAtivacao()
        {
            IsAtiva = IsAtiva == 1 ? 0 : 1;
            DataUltimaAtualizacao = DateTime.UtcNow;
        }

        public override string ToString()
        {
            //Esse método foi criado para haver uma representação legível para a empresa, concatenando a razão social e o CNPJ.
            //Somente o CNPJ fica pouco legível, e a razão social pode ser igual para filiais, logo os dois juntos trazem a representação completa.
            return $"{RazaoSocial} ({Cnpj})";
        }
    }
}

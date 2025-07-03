using System;

public class EmpresaDto
{
    public int IdEmpresa { get; set; }
    public int IsAtiva { get; set; }
    public string RazaoSocial { get; set; } = string.Empty;
    public string NomeFantasia { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public EnderecoDto EnderecoEmpresa { get; set; } = new EnderecoDto();
    public DateTime DataCriacao { get; set; }
    public DateTime DataUltimaAtualizacao { get; set; }
}


    public class EnderecoDto
    {
        public string Logradouro { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty; 
        public string Cep { get; set; } = string.Empty;
    }


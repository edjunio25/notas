using notas.Server.Backend.Domain.Enums;

namespace notas.Server.Backend.Domain.ValueObjects
{
    public class Endereco
    {
        public string logradouro { get; set; } = string.Empty;
        public string numero { get; set; } = string.Empty;
        public string complemento { get; set; } = string.Empty;

        public string bairro { get; set; } = string.Empty;
        public string cidade { get; set; } = string.Empty;
        public UF uf { get; set; }
        public string cep { get; set; } = string.Empty;

        public Endereco() { }

        public Endereco(string logradouroInput, string complementoInput,string numeroInput, string bairroInput, string cidadeInput, UF ufInput, string cepInput)
        {
            logradouro = logradouroInput;
            complemento = complementoInput ?? string.Empty;
            numero = numeroInput;
            bairro = bairroInput;
            cidade = cidadeInput;
            uf = ufInput;
            cep = cepInput;

            if (string.IsNullOrWhiteSpace(cep))
                throw new ArgumentException("CEP é obrigatório");

        }

        public override string ToString()
        {
            return $"{logradouro}, {numero} - {bairro}, {cidade} - {uf}, {cep}";
        }
    }
}

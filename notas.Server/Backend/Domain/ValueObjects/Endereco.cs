using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace notas.Server.Backend.Domain.ValueObjects
{
    [Owned]
    public class Endereco
    {
        public string logradouro { get; set; } = string.Empty;
        public string numero { get; set; } = string.Empty;
        public string complemento { get; set; } = string.Empty;

        public string bairro { get; set; } = string.Empty;
        public string cidade { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
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
            cep = RemoverCaracteresEspeciais(cepInput);

            if (string.IsNullOrWhiteSpace(cep))
                throw new ArgumentException("CEP é obrigatório");

            if (!ValidarCep(cep))
            {
                throw new ArgumentException("CEP Inválido");
            }

        }

        private static string RemoverCaracteresEspeciais(string inputCep)
        {
            return Regex.Replace(inputCep ?? "", "[^0-9]", "");
        }

        public static bool ValidarCep(string cep)
        {
            if (cep.Length != 8) return false;
            return Regex.IsMatch(cep, @"^\d{8}$");
        }

        public override string ToString()
        {
            return $"{logradouro}, {numero} - {bairro}, {cidade} - {uf}, {cep}";
        }
    }
}

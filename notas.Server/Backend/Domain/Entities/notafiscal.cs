using notas.Server.Backend.Domain.Enums;
using System;

namespace notas.Server.Backend.Domain.Entities
{
    public class NotaFiscal
    {
        public int IdNota { get; private set; }

        public Empresa EmpresaOrigem { get; private set; }
        public Empresa EmpresaDestino { get; private set; }

        public string NumeroNota { get; private set; }
        public string ChaveAcesso { get; private set; }
        public string Serie { get; private set; }
        public TipoNota TipoNota { get; private set; }

        public decimal ValorTotal { get; private set; }

        public DateTime DataEmissao { get; private set; }
        public DateTime DataPostagem { get; private set; }

        public string Descricao { get; private set; }

        protected NotaFiscal() { }

        public NotaFiscal(
            Empresa origem,
            Empresa destino,
            string numeroNota,
            string chaveAcesso,
            string serie,
            TipoNota tipoNota,
            decimal valorTotal,
            DateTime dataEmissao,
            DateTime dataPostagem,
            string descricao)
        {
            if (origem == null) throw new ArgumentNullException(nameof(origem));
            if (destino == null) throw new ArgumentNullException(nameof(destino));
            if (string.IsNullOrWhiteSpace(numeroNota)) throw new ArgumentException("Número da nota é obrigatório.");
            if (valorTotal <= 0) throw new ArgumentException("Valor total deve ser maior que zero.");

            EmpresaOrigem = origem;
            EmpresaDestino = destino;
            NumeroNota = numeroNota;
            ChaveAcesso = chaveAcesso ?? string.Empty;
            Serie = serie ?? string.Empty;
            TipoNota = tipoNota;
            ValorTotal = valorTotal;
            DataEmissao = dataEmissao;
            DataPostagem = dataPostagem;
            Descricao = descricao ?? string.Empty;
        }

        public override string ToString()
        {
            return $"{TipoNota} {NumeroNota} - {ValorTotal:C} ({DataEmissao:dd/MM/yyyy})";
        }
    }
}

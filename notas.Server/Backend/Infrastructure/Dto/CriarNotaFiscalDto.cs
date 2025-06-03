using System;
using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.ValueObjects;

namespace notas.Server.Backend.Infrastructure.Dto
{
    public class CriarNotaFiscalDto
    {
        public string NumeroNota { get; set; } = string.Empty;
        public string ChaveAcesso { get; set; } = string.Empty;
        public string Serie { get; set; } = string.Empty;
        public TipoNota TipoNota { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataPostagem { get; set; }
        public string Descricao { get; set; } = string.Empty;

        public string CnpjOrigem { get; set; } = string.Empty;
        public string CnpjDestino { get; set; } = string.Empty;
    }
}

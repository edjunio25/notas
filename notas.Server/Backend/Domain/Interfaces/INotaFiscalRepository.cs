using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using notas.Server.Backend.Domain.Entities;

namespace notas.Server.Backend.Domain.Interfaces
{
    public interface INotaFiscalRepository
    {
        Task SalvarAsync(NotaFiscal notaFiscal);
        Task<IEnumerable<NotaFiscal>> ListarAsync();
        Task<NotaFiscal?> BuscarPorNumeroAsync(string numeroNota);
    }
}
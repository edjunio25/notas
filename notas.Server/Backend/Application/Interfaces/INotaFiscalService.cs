using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace notas.Server.Backend.Application.Interfaces
{
    public interface INotaFiscalService
    {
        Task<NotaFiscal?> CriarNotaFiscalAsync(CriarNotaFiscalDto dto);
        Task<IEnumerable<NotaFiscal>> ListarNotasAsync();
        Task<bool> ExcluirNotaAsync(int id);
        Task<NotaFiscal?> BuscarPorIdAsync(int id);
    }
}

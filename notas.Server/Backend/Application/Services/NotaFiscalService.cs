using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using notas.Server.Backend.Application.Interfaces;

namespace notas.Server.Backend.Application.Services
{
    public class NotaFiscalService : INotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository, IEmpresaRepository empresaRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
            _empresaRepository = empresaRepository;
        }

        public virtual async Task<NotaFiscal?> CriarNotaFiscalAsync(CriarNotaFiscalDto dto)
        {
            var origem = await _empresaRepository.BuscarPorIdAsync(dto.EmpresaOrigemIdEmpresa);
            var destino = await _empresaRepository.BuscarPorIdAsync(dto.EmpresaDestinoIdEmpresa);

            if (origem == null || destino == null) return null;

            var nota = new NotaFiscal(
                origem,
                destino,
                dto.NumeroNota,
                dto.ChaveAcesso,
                dto.Serie,
                dto.TipoNota,
                dto.ValorTotal,
                dto.DataEmissao,
                dto.DataPostagem,
                dto.Descricao
            );

            await _notaFiscalRepository.SalvarAsync(nota);
            return nota;
        }

        public virtual async Task<IEnumerable<NotaFiscal>> ListarNotasAsync()
        {
            return await _notaFiscalRepository.ListarAsync();
        }

        public virtual async Task<bool> ExcluirNotaAsync(int id)
        {
            var nota = await _notaFiscalRepository.BuscarPorIdAsync(id);
            if (nota == null) return false;

            await _notaFiscalRepository.ExcluirAsync(nota);
            return true;
        }

        public virtual async Task<NotaFiscal?> BuscarPorIdAsync(int id)
        {
            return await _notaFiscalRepository.BuscarPorIdAsync(id);
        }



    }
}
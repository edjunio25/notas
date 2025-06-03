using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace notas.Server.Backend.Application.Services
{
    public class NotaFiscalService
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public NotaFiscalService(INotaFiscalRepository notaFiscalRepository, IEmpresaRepository empresaRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task<NotaFiscal?> CriarNotaFiscalAsync(CriarNotaFiscalDto dto)
        {
            var origem = await _empresaRepository.BuscarPorCnpjAsync(dto.CnpjOrigem);
            var destino = await _empresaRepository.BuscarPorCnpjAsync(dto.CnpjDestino);

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

        public async Task<IEnumerable<NotaFiscal>> ListarNotasAsync()
        {
            return await _notaFiscalRepository.ListarAsync();
        }
    }
}
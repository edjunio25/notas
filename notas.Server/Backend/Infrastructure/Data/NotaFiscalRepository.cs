using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace notas.Server.Backend.Infrastructure.Data
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly AppDbContext _context;

        public NotaFiscalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SalvarAsync(NotaFiscal notaFiscal)
        {
            _context.NotasFiscais.Add(notaFiscal);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotaFiscal>> ListarAsync()
        {
            return await _context.NotasFiscais.ToListAsync();
        }

        public async Task<NotaFiscal?> BuscarPorNumeroAsync(string numeroNota)
        {
            return await _context.NotasFiscais
                .FirstOrDefaultAsync(n => n.NumeroNota == numeroNota);
        }
    }
}
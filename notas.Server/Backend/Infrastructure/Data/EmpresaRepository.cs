using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using notas.Server.Backend.Infrastructure.Data;


namespace notas.Server.Backend.Infrastructure.Data
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly AppDbContext _context;

        public EmpresaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SalvarAsync(Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<Empresa?> BuscarPorCnpjAsync(string cnpj)
        {
            return await _context.Empresas
                .FirstOrDefaultAsync(e => e.Cnpj == cnpj);
        }

        public async Task<IEnumerable<Empresa>> ListarTodasAsync()
        {
            return await _context.Empresas.ToListAsync();
        }
    }
}


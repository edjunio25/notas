using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Entities;

namespace notas.Server.Backend.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
    }
}

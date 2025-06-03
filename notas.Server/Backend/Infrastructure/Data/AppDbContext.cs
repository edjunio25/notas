using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;

namespace SeuProjeto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
    }
}

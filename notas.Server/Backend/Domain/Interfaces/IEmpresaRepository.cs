using notas.Server.Backend.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace notas.Server.Backend.Domain.Interfaces
{
    public interface IEmpresaRepository
    {
        Task SalvarAsync(Empresa empresa);
        Task<Empresa?> BuscarPorCnpjAsync(string cnpj);
        Task<IEnumerable<Empresa>> ListarTodasAsync();
        Task<Empresa?> BuscarPorIdAsync(int id);
        Task AtualizarAsync(Empresa empresa);

    }
}
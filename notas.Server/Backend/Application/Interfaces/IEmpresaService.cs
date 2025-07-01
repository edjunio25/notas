using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace notas.Server.Backend.Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<Empresa?> CriarEmpresaAsync(CriarEmpresaDto dto);
        Task<IEnumerable<Empresa>> ListarEmpresasAsync();
        Task<bool> DesativarEmpresaAsync(int id);
    }
}

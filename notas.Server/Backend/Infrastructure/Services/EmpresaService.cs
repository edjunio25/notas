using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace notas.Server.Application.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Empresa?> CriarEmpresaAsync(CriarEmpresaDto dto)
        {
            var jaExiste = await _repository.BuscarPorCnpjAsync(dto.Cnpj);
            if (jaExiste != null) return null;

            var empresa = new Empresa(dto.RazaoSocial, dto.NomeFantasia, dto.Cnpj, dto.Endereco);
            await _repository.SalvarAsync(empresa);
            return empresa;
        }

        public async Task<IEnumerable<Empresa>> ListarEmpresasAsync()
        {
            return await _repository.ListarTodasAsync();
        }
    }
}

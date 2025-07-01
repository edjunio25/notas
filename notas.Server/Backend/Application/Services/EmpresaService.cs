using Microsoft.EntityFrameworkCore;
using notas.Server.Backend.Domain.Entities;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Infrastructure.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using notas.Server.Backend.Application.Interfaces;

namespace notas.Server.Backend.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public virtual async Task<Empresa?> CriarEmpresaAsync(CriarEmpresaDto dto)
        {
            var jaExiste = await _repository.BuscarPorCnpjAsync(dto.Cnpj);
            if (jaExiste != null) return null;

            var empresa = new Empresa(dto.RazaoSocial, dto.NomeFantasia, dto.Cnpj, dto.Endereco);
            await _repository.SalvarAsync(empresa);
            return empresa;
        }

        public virtual async Task<IEnumerable<Empresa>> ListarEmpresasAsync()
        {
            return await _repository.ListarTodasAsync();
        }

        public virtual async Task<bool> DesativarEmpresaAsync(int id)
        {
            var empresa = await _repository.BuscarPorIdAsync(id);
            if (empresa == null) return false;

            empresa.AlternarAtivacao();

            await _repository.AtualizarAsync(empresa);
            return true;
        }



    }
}

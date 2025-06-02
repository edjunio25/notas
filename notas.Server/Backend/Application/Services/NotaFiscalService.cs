using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Domain.ValueObjects;

namespace notas.Server.Backend.Application.Services
{
    public class NotaFiscalService
    {
        private readonly ICepService _cepService;

        public NotaFiscalService(ICepService cepService)
        {
            _cepService = cepService;
        }

        public async Task<Endereco?> MontarEnderecoComCep(string cep, string numero, string complemento)
        {
            var enderecoBase = await _cepService.ObterEnderecoPorCepAsync(cep);
            if (enderecoBase == null) return null;

            enderecoBase.numero = numero;
            enderecoBase.complemento = complemento; // se você adicionar esse campo

            return enderecoBase;
        }
    }

}

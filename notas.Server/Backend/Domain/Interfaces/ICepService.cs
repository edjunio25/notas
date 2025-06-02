using notas.Server.Backend.Domain.ValueObjects;

namespace notas.Server.Backend.Domain.Interfaces
{
    public interface ICepService
    {
        Task<Endereco?> ObterEnderecoPorCepAsync(string cep);
    }
}

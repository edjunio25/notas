using notas.Server.Backend.Domain.Enums;
using notas.Server.Backend.Domain.Interfaces;
using notas.Server.Backend.Domain.ValueObjects;
using notas.Server.Backend.Infrastructure.Dto;
using System.Text.Json;

namespace notas.Server.Backend.Infrastructure.Services
{
    public class ViaCepService : ICepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Endereco?> ObterEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (!response.IsSuccessStatusCode) return null;

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<ViaCepResponse>(content);

            if (data == null || data.Uf == null || !Enum.TryParse<UF>(data.Uf, out var uf))
                return null;

            return new Endereco(
                logradouroInput: data.Logradouro ?? "",
                complementoInput: "",
                numeroInput: "",
                bairroInput: data.Bairro ?? "",
                cidadeInput: data.Localidade ?? "",
                ufInput: uf,
                cepInput: data.Cep ?? "");
        }
    }
}

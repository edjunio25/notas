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
            try
            {
                Console.WriteLine($"Fazendo chamada para: https://viacep.com.br/ws/{cep}/json/");
                var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                if (!response.IsSuccessStatusCode) return null;
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Conteúdo bruto do ViaCEP:");
                Console.WriteLine(content);
                var data = JsonSerializer.Deserialize<ViaCepResponse>(content);

                System.Diagnostics.Debugger.Break();

                if (data == null || string.IsNullOrWhiteSpace(data.Uf))
                {
                    Console.WriteLine($"Erro: dados do ViaCEP estão incompletos.");
                    return null;
                }

                if (!Enum.TryParse<UF>(data.Uf.Trim(), ignoreCase: true, out var uf))
                {
                    Console.WriteLine($"Erro: UF '{data.Uf}' não pôde ser convertida para enum UF.");
                    return null;
                }



                Console.WriteLine($"UF recebido do ViaCEP: {data.Uf}");

                return new Endereco(
                    logradouroInput: data.Logradouro ?? "",
                    complementoInput: "",
                    numeroInput: "",
                    bairroInput: data.Bairro ?? "",
                    cidadeInput: data.Localidade ?? "",
                    ufInput: uf,
                    cepInput: data.Cep ?? "");
            }
            catch
            {
                return null;
            }
        }

    }
}

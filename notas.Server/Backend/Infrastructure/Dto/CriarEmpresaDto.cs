using notas.Server.Backend.Domain.ValueObjects;

namespace notas.Server.Backend.Infrastructure.Dto
{
    public class CriarEmpresaDto
    {
        public string RazaoSocial { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public Endereco Endereco { get; set; } = new Endereco();
    }
}

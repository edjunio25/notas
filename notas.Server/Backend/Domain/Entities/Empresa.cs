namespace notas.Server.Backend.Domain.Entities
{
    public class Empresa
    {
        private string idEmpresa { get; }
        private string razaoSocialEmpresa { get; set; }
        private string nomeFantasiaEmpresa { get; set; }
        private string cnpjEmpresa { get; set; }
        private DateTime dataCriacaoEmpresa { get; }
        private DateTime dataUltimaAtualizacao { get; set; }
    }
}

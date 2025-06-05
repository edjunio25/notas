using System.ComponentModel;
namespace notas.Server.Backend.Domain.Enums
{
    public enum TipoNota
    {
        [Description("Nota Fiscal de Serviços")]
        NFS,

        [Description("Nota Fiscal Eletrônica")]
        NFE,

        [Description("Conhecimento de Transporte Eletrônico")]
        CTE
    }
}
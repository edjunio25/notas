using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace notas.Server.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    IdEmpresa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsAtiva = table.Column<int>(type: "INTEGER", nullable: false),
                    RazaoSocial = table.Column<string>(type: "TEXT", nullable: false),
                    NomeFantasia = table.Column<string>(type: "TEXT", nullable: false),
                    Cnpj = table.Column<string>(type: "TEXT", nullable: false),
                    EnderecoEmpresa_logradouro = table.Column<string>(type: "TEXT", nullable: false),
                    EnderecoEmpresa_numero = table.Column<string>(type: "TEXT", nullable: false),
                    EnderecoEmpresa_complemento = table.Column<string>(type: "TEXT", nullable: false),
                    EnderecoEmpresa_bairro = table.Column<string>(type: "TEXT", nullable: false),
                    EnderecoEmpresa_cidade = table.Column<string>(type: "TEXT", nullable: false),
                    EnderecoEmpresa_uf = table.Column<int>(type: "INTEGER", nullable: false),
                    EnderecoEmpresa_cep = table.Column<string>(type: "TEXT", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataUltimaAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.IdEmpresa);
                });

            migrationBuilder.CreateTable(
                name: "NotasFiscais",
                columns: table => new
                {
                    IdNota = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmpresaOrigemIdEmpresa = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpresaDestinoIdEmpresa = table.Column<int>(type: "INTEGER", nullable: false),
                    NumeroNota = table.Column<string>(type: "TEXT", nullable: false),
                    ChaveAcesso = table.Column<string>(type: "TEXT", nullable: false),
                    Serie = table.Column<string>(type: "TEXT", nullable: false),
                    TipoNota = table.Column<int>(type: "INTEGER", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "TEXT", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataPostagem = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasFiscais", x => x.IdNota);
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Empresas_EmpresaDestinoIdEmpresa",
                        column: x => x.EmpresaDestinoIdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Empresas_EmpresaOrigemIdEmpresa",
                        column: x => x.EmpresaOrigemIdEmpresa,
                        principalTable: "Empresas",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaDestinoIdEmpresa",
                table: "NotasFiscais",
                column: "EmpresaDestinoIdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaOrigemIdEmpresa",
                table: "NotasFiscais",
                column: "EmpresaOrigemIdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotasFiscais");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}

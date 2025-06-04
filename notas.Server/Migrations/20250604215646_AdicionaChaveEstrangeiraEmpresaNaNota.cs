using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace notas.Server.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaChaveEstrangeiraEmpresaNaNota : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaDestinoIdEmpresa",
                table: "NotasFiscais");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaOrigemIdEmpresa",
                table: "NotasFiscais");

            migrationBuilder.RenameColumn(
                name: "EmpresaOrigemIdEmpresa",
                table: "NotasFiscais",
                newName: "EmpresaOrigemId");

            migrationBuilder.RenameColumn(
                name: "EmpresaDestinoIdEmpresa",
                table: "NotasFiscais",
                newName: "EmpresaDestinoId");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_EmpresaOrigemIdEmpresa",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_EmpresaOrigemId");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_EmpresaDestinoIdEmpresa",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_EmpresaDestinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaDestinoId",
                table: "NotasFiscais",
                column: "EmpresaDestinoId",
                principalTable: "Empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaOrigemId",
                table: "NotasFiscais",
                column: "EmpresaOrigemId",
                principalTable: "Empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaDestinoId",
                table: "NotasFiscais");

            migrationBuilder.DropForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaOrigemId",
                table: "NotasFiscais");

            migrationBuilder.RenameColumn(
                name: "EmpresaOrigemId",
                table: "NotasFiscais",
                newName: "EmpresaOrigemIdEmpresa");

            migrationBuilder.RenameColumn(
                name: "EmpresaDestinoId",
                table: "NotasFiscais",
                newName: "EmpresaDestinoIdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_EmpresaOrigemId",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_EmpresaOrigemIdEmpresa");

            migrationBuilder.RenameIndex(
                name: "IX_NotasFiscais_EmpresaDestinoId",
                table: "NotasFiscais",
                newName: "IX_NotasFiscais_EmpresaDestinoIdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaDestinoIdEmpresa",
                table: "NotasFiscais",
                column: "EmpresaDestinoIdEmpresa",
                principalTable: "Empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NotasFiscais_Empresas_EmpresaOrigemIdEmpresa",
                table: "NotasFiscais",
                column: "EmpresaOrigemIdEmpresa",
                principalTable: "Empresas",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

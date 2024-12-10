using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoCaninaApp.Infra.Data.Migrations
{
    public partial class _10122024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Cao",
                table: "Fotos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricoSaude_Cao",
                table: "HistoricosDeSaude");

            migrationBuilder.RenameColumn(
                name: "Feedback",
                table: "Sugestoes",
                newName: "FeedBack");

            migrationBuilder.AddColumn<string>(
                name: "CaminhoFoto",
                table: "Caes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Cao",
                table: "Fotos",
                column: "CaoId",
                principalTable: "Caes",
                principalColumn: "CaoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricosDeSaude_Cao",
                table: "HistoricosDeSaude",
                column: "CaoId",
                principalTable: "Caes",
                principalColumn: "CaoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Cao",
                table: "Fotos");

            migrationBuilder.DropForeignKey(
                name: "FK_HistoricosDeSaude_Cao",
                table: "HistoricosDeSaude");

            migrationBuilder.DropColumn(
                name: "CaminhoFoto",
                table: "Caes");

            migrationBuilder.RenameColumn(
                name: "FeedBack",
                table: "Sugestoes",
                newName: "Feedback");

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Cao",
                table: "Fotos",
                column: "CaoId",
                principalTable: "Caes",
                principalColumn: "CaoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HistoricoSaude_Cao",
                table: "HistoricosDeSaude",
                column: "CaoId",
                principalTable: "Caes",
                principalColumn: "CaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

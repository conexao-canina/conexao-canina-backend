using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoCaninaApp.Infra.Data.Migrations
{
    public partial class AddingNewEntities0210 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaracteristicasUnicas",
                table: "Caes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Caes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Tamanho",
                table: "Caes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    FotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaminhoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    CaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.FotoId);
                    table.ForeignKey(
                        name: "FK_Fotos_Caes_CaoId",
                        column: x => x.CaoId,
                        principalTable: "Caes",
                        principalColumn: "CaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosDeSaude",
                columns: table => new
                {
                    HistoricoSaudeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exame = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vacinas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CondicoesDeSaude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsentimentoDono = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosDeSaude", x => x.HistoricoSaudeId);
                    table.ForeignKey(
                        name: "FK_HistoricosDeSaude_Caes_CaoId",
                        column: x => x.CaoId,
                        principalTable: "Caes",
                        principalColumn: "CaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_CaoId",
                table: "Fotos",
                column: "CaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosDeSaude_CaoId",
                table: "HistoricosDeSaude",
                column: "CaoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "HistoricosDeSaude");

            migrationBuilder.DropColumn(
                name: "CaracteristicasUnicas",
                table: "Caes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Caes");

            migrationBuilder.DropColumn(
                name: "Tamanho",
                table: "Caes");
        }
    }
}

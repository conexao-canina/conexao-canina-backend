using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoCaninaApp.Infra.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Caes",
                columns: table => new
                {
                    CaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Raca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaracteristicasUnicas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tamanho = table.Column<int>(type: "int", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caes", x => x.CaoId);
                    table.ForeignKey(
                        name: "FK_Cao_Usuario",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Sugestoes",
                columns: table => new
                {
                    SugestaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sugestoes", x => x.SugestaoId);
                    table.ForeignKey(
                        name: "FK_Sugestao_Usuario",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    FavoritoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.FavoritoId);
                    table.ForeignKey(
                        name: "FK_Favoritos_Cao",
                        column: x => x.CaoId,
                        principalTable: "Caes",
                        principalColumn: "CaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_Usuario",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "UsuarioId");
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    FotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaminhoArquivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.FotoId);
                    table.ForeignKey(
                        name: "FK_Foto_Cao",
                        column: x => x.CaoId,
                        principalTable: "Caes",
                        principalColumn: "CaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosDeSaude",
                columns: table => new
                {
                    HistoricoSaudeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Exame = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Vacina = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CondicoesDeSaude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConsentimentoDono = table.Column<bool>(type: "bit", nullable: false),
                    DataExame = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosDeSaude", x => x.HistoricoSaudeId);
                    table.ForeignKey(
                        name: "FK_HistoricoSaude_Cao",
                        column: x => x.CaoId,
                        principalTable: "Caes",
                        principalColumn: "CaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caes_UsuarioId",
                table: "Caes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_CaoId",
                table: "Favoritos",
                column: "CaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UsuarioId",
                table: "Favoritos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_CaoId",
                table: "Fotos",
                column: "CaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosDeSaude_CaoId",
                table: "HistoricosDeSaude",
                column: "CaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sugestoes_UsuarioId",
                table: "Sugestoes",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "HistoricosDeSaude");

            migrationBuilder.DropTable(
                name: "Sugestoes");

            migrationBuilder.DropTable(
                name: "Caes");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}

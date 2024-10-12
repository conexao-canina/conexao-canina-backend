using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoCaninaApp.Infra.Data.Migrations
{
    public partial class AddingNewEntities1110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Fotos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Fotos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RequisitosCruzamentoId",
                table: "Caes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Albuns",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProprietarioId = table.Column<int>(type: "int", nullable: false),
                    Privacidade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albuns", x => x.AlbumId);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataLike = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_Likes_Caes_CaoId",
                        column: x => x.CaoId,
                        principalTable: "Caes",
                        principalColumn: "CaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequisitosCruzamentos",
                columns: table => new
                {
                    RequisitosCruzamentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamanho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaracteristicasGeneticas = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitosCruzamentos", x => x.RequisitosCruzamentoId);
                });

            migrationBuilder.CreateTable(
                name: "SolicitacoesCruzamento",
                columns: table => new
                {
                    SolicitacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CaoId = table.Column<int>(type: "int", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacoesCruzamento", x => x.SolicitacaoId);
                    table.ForeignKey(
                        name: "FK_SolicitacoesCruzamento_Caes_CaoId",
                        column: x => x.CaoId,
                        principalTable: "Caes",
                        principalColumn: "CaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacoesCruzamento_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sugestoes",
                columns: table => new
                {
                    SugestaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sugestoes", x => x.SugestaoId);
                    table.ForeignKey(
                        name: "FK_Sugestoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AlbumId",
                table: "Usuarios",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_AlbumId",
                table: "Fotos",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Caes_RequisitosCruzamentoId",
                table: "Caes",
                column: "RequisitosCruzamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CaoId",
                table: "Likes",
                column: "CaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesCruzamento_CaoId",
                table: "SolicitacoesCruzamento",
                column: "CaoId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacoesCruzamento_UsuarioId",
                table: "SolicitacoesCruzamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sugestoes_UsuarioId",
                table: "Sugestoes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caes_RequisitosCruzamentos_RequisitosCruzamentoId",
                table: "Caes",
                column: "RequisitosCruzamentoId",
                principalTable: "RequisitosCruzamentos",
                principalColumn: "RequisitosCruzamentoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Albuns_AlbumId",
                table: "Fotos",
                column: "AlbumId",
                principalTable: "Albuns",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Albuns_AlbumId",
                table: "Usuarios",
                column: "AlbumId",
                principalTable: "Albuns",
                principalColumn: "AlbumId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caes_RequisitosCruzamentos_RequisitosCruzamentoId",
                table: "Caes");

            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Albuns_AlbumId",
                table: "Fotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Albuns_AlbumId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Albuns");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "RequisitosCruzamentos");

            migrationBuilder.DropTable(
                name: "SolicitacoesCruzamento");

            migrationBuilder.DropTable(
                name: "Sugestoes");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_AlbumId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Fotos_AlbumId",
                table: "Fotos");

            migrationBuilder.DropIndex(
                name: "IX_Caes_RequisitosCruzamentoId",
                table: "Caes");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "RequisitosCruzamentoId",
                table: "Caes");
        }
    }
}

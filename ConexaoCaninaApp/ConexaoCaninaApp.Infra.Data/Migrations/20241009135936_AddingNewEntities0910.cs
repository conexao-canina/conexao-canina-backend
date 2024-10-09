using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConexaoCaninaApp.Infra.Data.Migrations
{
    public partial class AddingNewEntities0910 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Albuns",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProprietarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albuns", x => x.AlbumId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_AlbumId",
                table: "Fotos",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotos_Albuns_AlbumId",
                table: "Fotos",
                column: "AlbumId",
                principalTable: "Albuns",
                principalColumn: "AlbumId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotos_Albuns_AlbumId",
                table: "Fotos");

            migrationBuilder.DropTable(
                name: "Albuns");

            migrationBuilder.DropIndex(
                name: "IX_Fotos_AlbumId",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Fotos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Fotos");
        }
    }
}

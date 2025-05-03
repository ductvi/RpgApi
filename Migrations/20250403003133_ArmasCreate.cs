using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class ArmasCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Armas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonagemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Armas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Armas",
                columns: new[] { "Id", "Dano", "Nome", "PersonagemId", "Tipo" },
                values: new object[,]
                {
                    { 1, 24436, "Eduardo Dinande", 1, "Espada" },
                    { 2, 18500, "Samurai's Blade", 2, "Lança" },
                    { 3, 12000, "Light of Galadriel", 3, "Machado" },
                    { 4, 9000, "Gandalf's Staff", 4, "Cajado" },
                    { 5, 7000, "Hobbit's Slingshot", 5, "Estilingue" },
                    { 6, 14500, "Celdron's Mace", 6, "Mace" },
                    { 7, 11000, "Radagast's Hammer", 7, "Martelo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Armas");
        }
    }
}

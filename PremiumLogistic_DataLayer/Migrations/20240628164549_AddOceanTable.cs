using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumLogistic_DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddOceanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Oceans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Savannah = table.Column<int>(type: "int", nullable: false),
                    Elizabeth = table.Column<int>(type: "int", nullable: false),
                    Houston = table.Column<int>(type: "int", nullable: false),
                    LosAngeles = table.Column<int>(type: "int", nullable: false),
                    Indianapolis = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oceans", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Oceans");

        }
    }
}

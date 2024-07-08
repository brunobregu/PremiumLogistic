using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumLogistic_DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddToBePaidColum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ToBePaid",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToBePaid",
                table: "OrderDetails");
        }
    }
}

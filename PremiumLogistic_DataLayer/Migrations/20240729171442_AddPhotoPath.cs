using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumLogistic_DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Storage",
                table: "OrderDetails",
                newName: "StorageCost");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CarStatus",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClientStorage",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DocumentsPath",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotosPath",
                table: "OrderDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientStorage",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "DocumentsPath",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "PhotosPath",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "StorageCost",
                table: "OrderDetails",
                newName: "Storage");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentStatus",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CarStatus",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

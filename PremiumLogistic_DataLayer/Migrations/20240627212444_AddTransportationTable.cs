using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumLogistic_DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddTransportationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transportation",
                columns: table => new
                {
                    Zip = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    AuctionLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Auction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Savannah = table.Column<int>(type: "int", nullable: false),
                    Elizabeth = table.Column<int>(type: "int", nullable: false),
                    Houston = table.Column<int>(type: "int", nullable: false),
                    LosAngeles = table.Column<int>(type: "int", nullable: false),
                    Indianapolis = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportation", x => x.Zip);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalTransportations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Auction = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AuctionLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Elizabeth_NJ = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Houston_TX = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Indianapolis_IN = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    LosAngeles_CA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Savannah_GA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalTransportations", x => x.Id);
                });
        }
    }
}

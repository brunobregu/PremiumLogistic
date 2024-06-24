using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PremiumLogistic_DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class changeAplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedIP",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "UpdatedIP",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "CreatedIP",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedIP",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "CreatedIP",
                table: "LocalTransportations");

            migrationBuilder.DropColumn(
                name: "UpdatedIP",
                table: "LocalTransportations");

            migrationBuilder.DropColumn(
                name: "CreatedIP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedIP",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedIP",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "UpdatedIP",
                table: "AspNetUsers",
                newName: "TemporaryPassword");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "LocalTransportations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LocalTransportations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BodyRequest",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "TemporaryPasswordExpiration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedOn",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyRequest",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "TemporaryPasswordExpiration",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TemporaryPassword",
                table: "AspNetUsers",
                newName: "UpdatedIP");

            migrationBuilder.AddColumn<string>(
                name: "CreatedIP",
                table: "Ports",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedIP",
                table: "Ports",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedIP",
                table: "OrderDetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedIP",
                table: "OrderDetails",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedBy",
                table: "LocalTransportations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "LocalTransportations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedIP",
                table: "LocalTransportations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedIP",
                table: "LocalTransportations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "CreatedIP",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedOn",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedOn",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedIP",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedIP",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

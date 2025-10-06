using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ClientUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_ClienteId",
                schema: "bnk",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                schema: "bnk",
                table: "Clientes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                schema: "bnk",
                table: "Clientes",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_Identificacion",
                schema: "bnk",
                table: "Clientes",
                column: "Identificacion",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_Identificacion",
                schema: "bnk",
                table: "Clientes");

            migrationBuilder.AlterColumn<string>(
                name: "FechaNacimiento",
                schema: "bnk",
                table: "Clientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "ClienteId",
                schema: "bnk",
                table: "Clientes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ClienteId",
                schema: "bnk",
                table: "Clientes",
                column: "ClienteId",
                unique: true);
        }
    }
}

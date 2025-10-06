using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bnk");

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "bnk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClienteId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cuentas",
                schema: "bnk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroCuenta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TipoCuenta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SaldoInicial = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    FechaApertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClienteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cuentas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "bnk",
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                schema: "bnk",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                    CuentaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Cuentas_CuentaId",
                        column: x => x.CuentaId,
                        principalSchema: "bnk",
                        principalTable: "Cuentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ClienteId",
                schema: "bnk",
                table: "Clientes",
                column: "ClienteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_ClienteId",
                schema: "bnk",
                table: "Cuentas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_NumeroCuenta",
                schema: "bnk",
                table: "Cuentas",
                column: "NumeroCuenta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_CuentaId",
                schema: "bnk",
                table: "Movimientos",
                column: "CuentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos",
                schema: "bnk");

            migrationBuilder.DropTable(
                name: "Cuentas",
                schema: "bnk");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "bnk");
        }
    }
}

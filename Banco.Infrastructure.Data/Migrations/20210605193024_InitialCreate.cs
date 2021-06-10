using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Banco.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consecutivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroInicial = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consecutivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuentasBancarias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentasBancarias", x => x.Id);
                    table.UniqueConstraint("AK_CuentasBancarias_Numero", x => x.Numero);
                });

            migrationBuilder.CreateTable(
                name: "ConsecutivoLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConsecutivoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsecutivoLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsecutivoLog_Consecutivo_ConsecutivoId",
                        column: x => x.ConsecutivoId,
                        principalTable: "Consecutivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoFinanciero",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaBancariaId = table.Column<int>(type: "int", nullable: true),
                    ValorRetiro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ValorConsignacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoFinanciero", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientoFinanciero_CuentasBancarias_CuentaBancariaId",
                        column: x => x.CuentaBancariaId,
                        principalTable: "CuentasBancarias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Consecutivo",
                columns: new[] { "Id", "Numero", "NumeroInicial" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ConsecutivoLog_ConsecutivoId",
                table: "ConsecutivoLog",
                column: "ConsecutivoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoFinanciero_CuentaBancariaId",
                table: "MovimientoFinanciero",
                column: "CuentaBancariaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsecutivoLog");

            migrationBuilder.DropTable(
                name: "MovimientoFinanciero");

            migrationBuilder.DropTable(
                name: "Consecutivo");

            migrationBuilder.DropTable(
                name: "CuentasBancarias");
        }
    }
}

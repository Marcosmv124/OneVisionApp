using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace One_Vision.Migrations
{
    /// <inheritdoc />
    public partial class AddVentasYDetalle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    ID_Venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Paciente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Abonado = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.ID_Venta);
                    table.ForeignKey(
                        name: "FK_Ventas_Pacientes_ID_Paciente",
                        column: x => x.ID_Paciente,
                        principalTable: "Pacientes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VentaProductos",
                columns: table => new
                {
                    ID_VentaProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Venta = table.Column<int>(type: "int", nullable: false),
                    CodigoDeBarra = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaProductos", x => x.ID_VentaProducto);
                    table.ForeignKey(
                        name: "FK_VentaProductos_Productos_CodigoDeBarra",
                        column: x => x.CodigoDeBarra,
                        principalTable: "Productos",
                        principalColumn: "CodigoDeBarra",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VentaProductos_Ventas_ID_Venta",
                        column: x => x.ID_Venta,
                        principalTable: "Ventas",
                        principalColumn: "ID_Venta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VentaProductos_CodigoDeBarra",
                table: "VentaProductos",
                column: "CodigoDeBarra");

            migrationBuilder.CreateIndex(
                name: "IX_VentaProductos_ID_Venta",
                table: "VentaProductos",
                column: "ID_Venta");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ID_Paciente",
                table: "Ventas",
                column: "ID_Paciente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VentaProductos");

            migrationBuilder.DropTable(
                name: "Ventas");
        }
    }
}

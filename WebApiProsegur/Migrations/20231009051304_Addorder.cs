using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiProsegur.Migrations
{
    /// <inheritdoc />
    public partial class Addorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DetalleOrdenes",
                columns: new[] { "Id", "Cantidad", "IdItems", "IdOrden" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 2, 4, 2, 1 },
                    { 3, 5, 3, 2 },
                    { 4, 3, 2, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Precio",
                value: 16.0);

            migrationBuilder.InsertData(
                table: "Ordenes",
                columns: new[] { "Id", "Cliente", "Estado", "IdTienda", "IdUsuario", "Impuesto", "SubTotal", "Total" },
                values: new object[,]
                {
                    { 1, "Susan", 1, 1, 2, 18.719999999999999, 104.0, 122.72 },
                    { 2, "Daniel", 1, 1, 2, 23.219999999999999, 129.0, 152.22 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DetalleOrdenes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DetalleOrdenes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DetalleOrdenes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DetalleOrdenes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ordenes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ordenes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "Precio",
                value: 16.5);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiProsegur.Migrations
{
    /// <inheritdoc />
    public partial class AddRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdRol",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rol",
                table: "Usuarios");
        }
    }
}

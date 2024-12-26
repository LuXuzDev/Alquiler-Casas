using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlquiler.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeTuMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "direccion",
                table: "CasasPendientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "CasasPendientes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "direccion",
                table: "Casas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Casas",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "direccion",
                table: "CasasPendientes");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "CasasPendientes");

            migrationBuilder.DropColumn(
                name: "direccion",
                table: "Casas");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Casas");
        }
    }
}

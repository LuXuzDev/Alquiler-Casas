using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAlquiler.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class agregarTotal_Reservacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casas_Ciudades_idCiudad",
                table: "Casas");

            migrationBuilder.AddColumn<double>(
                name: "costoTotal",
                table: "Reservaciones",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<int>(
                name: "idCiudad",
                table: "Casas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Casas_Ciudades_idCiudad",
                table: "Casas",
                column: "idCiudad",
                principalTable: "Ciudades",
                principalColumn: "idCiudad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Casas_Ciudades_idCiudad",
                table: "Casas");

            migrationBuilder.DropColumn(
                name: "costoTotal",
                table: "Reservaciones");

            migrationBuilder.AlterColumn<int>(
                name: "idCiudad",
                table: "Casas",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Casas_Ciudades_idCiudad",
                table: "Casas",
                column: "idCiudad",
                principalTable: "Ciudades",
                principalColumn: "idCiudad",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

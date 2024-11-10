using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SistemaAlquiler.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class primeraMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    correo = table.Column<string>(type: "text", nullable: false),
                    numeroContacto = table.Column<string>(type: "text", nullable: false),
                    clave = table.Column<string>(type: "text", nullable: false),
                    rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Casas",
                columns: table => new
                {
                    idCasa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<int>(type: "integer", nullable: false),
                    precioNoche = table.Column<double>(type: "double precision", nullable: false),
                    direccion = table.Column<string>(type: "text", nullable: false),
                    areaTotal = table.Column<double>(type: "double precision", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casas", x => x.idCasa);
                    table.ForeignKey(
                        name: "FK_Casas_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Caracteristicas",
                columns: table => new
                {
                    idCaracteristicas = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCasa = table.Column<int>(type: "integer", nullable: false),
                    cantMaxPersonas = table.Column<int>(type: "integer", nullable: false),
                    cantHabitaciones = table.Column<int>(type: "integer", nullable: false),
                    cantBanos = table.Column<int>(type: "integer", nullable: false),
                    cantCuartos = table.Column<int>(type: "integer", nullable: false),
                    cocina = table.Column<bool>(type: "boolean", nullable: false),
                    terraza_balcon = table.Column<bool>(type: "boolean", nullable: false),
                    barbacoa = table.Column<bool>(type: "boolean", nullable: false),
                    garaje = table.Column<bool>(type: "boolean", nullable: false),
                    piscina = table.Column<bool>(type: "boolean", nullable: false),
                    gimnasio = table.Column<bool>(type: "boolean", nullable: false),
                    lavadora_secadora = table.Column<bool>(type: "boolean", nullable: false),
                    tv = table.Column<bool>(type: "boolean", nullable: false),
                    permiteMenores = table.Column<bool>(type: "boolean", nullable: false),
                    permiteFumar = table.Column<bool>(type: "boolean", nullable: false),
                    permiteMascotas = table.Column<bool>(type: "boolean", nullable: false),
                    wifi = table.Column<bool>(type: "boolean", nullable: false),
                    aguaCaliente = table.Column<bool>(type: "boolean", nullable: false),
                    climatizada = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristicas", x => x.idCaracteristicas);
                    table.ForeignKey(
                        name: "FK_Caracteristicas_Casas_idCasa",
                        column: x => x.idCasa,
                        principalTable: "Casas",
                        principalColumn: "idCasa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    idFoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCasa = table.Column<int>(type: "integer", nullable: false),
                    direccionURL = table.Column<string>(type: "text", nullable: false),
                    direccionName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.idFoto);
                    table.ForeignKey(
                        name: "FK_Fotos_Casas_idCasa",
                        column: x => x.idCasa,
                        principalTable: "Casas",
                        principalColumn: "idCasa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservaciones",
                columns: table => new
                {
                    idReservacion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<int>(type: "integer", nullable: false),
                    idCasa = table.Column<int>(type: "integer", nullable: false),
                    cantPersonas = table.Column<int>(type: "integer", nullable: false),
                    fechaEntrada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fechaSalida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservaciones", x => x.idReservacion);
                    table.ForeignKey(
                        name: "FK_Reservaciones_Casas_idCasa",
                        column: x => x.idCasa,
                        principalTable: "Casas",
                        principalColumn: "idCasa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservaciones_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Valoraciones",
                columns: table => new
                {
                    idValoracion = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idUsuario = table.Column<int>(type: "integer", nullable: false),
                    idCasa = table.Column<int>(type: "integer", nullable: false),
                    puntuacion = table.Column<double>(type: "double precision", nullable: false),
                    comentario = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Valoraciones", x => x.idValoracion);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Casas_idCasa",
                        column: x => x.idCasa,
                        principalTable: "Casas",
                        principalColumn: "idCasa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Valoraciones_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caracteristicas_idCasa",
                table: "Caracteristicas",
                column: "idCasa");

            migrationBuilder.CreateIndex(
                name: "IX_Casas_idUsuario",
                table: "Casas",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_idCasa",
                table: "Fotos",
                column: "idCasa");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_idCasa",
                table: "Reservaciones",
                column: "idCasa");

            migrationBuilder.CreateIndex(
                name: "IX_Reservaciones_idUsuario",
                table: "Reservaciones",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_idCasa",
                table: "Valoraciones",
                column: "idCasa");

            migrationBuilder.CreateIndex(
                name: "IX_Valoraciones_idUsuario",
                table: "Valoraciones",
                column: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caracteristicas");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "Reservaciones");

            migrationBuilder.DropTable(
                name: "Valoraciones");

            migrationBuilder.DropTable(
                name: "Casas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}

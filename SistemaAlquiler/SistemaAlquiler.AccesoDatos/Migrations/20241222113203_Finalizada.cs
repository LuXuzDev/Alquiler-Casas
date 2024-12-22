using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SistemaAlquiler.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class Finalizada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caracteristicas",
                columns: table => new
                {
                    idCaracteristicas = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                });

            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    idCiudad = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ciudad = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.idCiudad);
                });

            migrationBuilder.CreateTable(
                name: "jwt_Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jwt_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rol = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.idRol);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idRol = table.Column<int>(type: "integer", nullable: false),
                    correo = table.Column<string>(type: "text", nullable: false),
                    numeroContacto = table.Column<string>(type: "text", nullable: false),
                    clave = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_idRol",
                        column: x => x.idRol,
                        principalTable: "Roles",
                        principalColumn: "idRol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Casas",
                columns: table => new
                {
                    idCasa = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCaracteristica = table.Column<int>(type: "integer", nullable: false),
                    idUsuario = table.Column<int>(type: "integer", nullable: true),
                    idCiudad = table.Column<int>(type: "integer", nullable: true),
                    precioNoche = table.Column<double>(type: "double precision", nullable: false),
                    precioMes = table.Column<double>(type: "double precision", nullable: false),
                    areaTotal = table.Column<double>(type: "double precision", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Casas", x => x.idCasa);
                    table.ForeignKey(
                        name: "FK_Casas_Caracteristicas_idCaracteristica",
                        column: x => x.idCaracteristica,
                        principalTable: "Caracteristicas",
                        principalColumn: "idCaracteristicas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Casas_Ciudades_idCiudad",
                        column: x => x.idCiudad,
                        principalTable: "Ciudades",
                        principalColumn: "idCiudad");
                    table.ForeignKey(
                        name: "FK_Casas_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "CasasPendientes",
                columns: table => new
                {
                    idCasaPendiente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCaracteristica = table.Column<int>(type: "integer", nullable: false),
                    idUsuario = table.Column<int>(type: "integer", nullable: true),
                    idCiudad = table.Column<int>(type: "integer", nullable: true),
                    CasaidCasa = table.Column<int>(type: "integer", nullable: false),
                    precioNoche = table.Column<double>(type: "double precision", nullable: false),
                    precioMes = table.Column<double>(type: "double precision", nullable: false),
                    areaTotal = table.Column<double>(type: "double precision", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasasPendientes", x => x.idCasaPendiente);
                    table.ForeignKey(
                        name: "FK_CasasPendientes_Caracteristicas_idCaracteristica",
                        column: x => x.idCaracteristica,
                        principalTable: "Caracteristicas",
                        principalColumn: "idCaracteristicas",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasasPendientes_Casas_CasaidCasa",
                        column: x => x.CasaidCasa,
                        principalTable: "Casas",
                        principalColumn: "idCasa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasasPendientes_Ciudades_idCiudad",
                        column: x => x.idCiudad,
                        principalTable: "Ciudades",
                        principalColumn: "idCiudad");
                    table.ForeignKey(
                        name: "FK_CasasPendientes_Usuarios_idUsuario",
                        column: x => x.idUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "idUsuario");
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
                    fechaEntrada = table.Column<DateOnly>(type: "date", nullable: false),
                    fechaSalida = table.Column<DateOnly>(type: "date", nullable: false),
                    costoTotal = table.Column<double>(type: "double precision", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    idFoto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    idCasa = table.Column<int>(type: "integer", nullable: false),
                    direccionURL = table.Column<string>(type: "text", nullable: false),
                    direccionName = table.Column<string>(type: "text", nullable: false),
                    CasaPendienteidCasaPendiente = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.idFoto);
                    table.ForeignKey(
                        name: "FK_Fotos_CasasPendientes_CasaPendienteidCasaPendiente",
                        column: x => x.CasaPendienteidCasaPendiente,
                        principalTable: "CasasPendientes",
                        principalColumn: "idCasaPendiente");
                    table.ForeignKey(
                        name: "FK_Fotos_Casas_idCasa",
                        column: x => x.idCasa,
                        principalTable: "Casas",
                        principalColumn: "idCasa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Casas_idCaracteristica",
                table: "Casas",
                column: "idCaracteristica");

            migrationBuilder.CreateIndex(
                name: "IX_Casas_idCiudad",
                table: "Casas",
                column: "idCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_Casas_idUsuario",
                table: "Casas",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_CasasPendientes_CasaidCasa",
                table: "CasasPendientes",
                column: "CasaidCasa");

            migrationBuilder.CreateIndex(
                name: "IX_CasasPendientes_idCaracteristica",
                table: "CasasPendientes",
                column: "idCaracteristica");

            migrationBuilder.CreateIndex(
                name: "IX_CasasPendientes_idCiudad",
                table: "CasasPendientes",
                column: "idCiudad");

            migrationBuilder.CreateIndex(
                name: "IX_CasasPendientes_idUsuario",
                table: "CasasPendientes",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Fotos_CasaPendienteidCasaPendiente",
                table: "Fotos",
                column: "CasaPendienteidCasaPendiente");

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
                name: "IX_Usuarios_idRol",
                table: "Usuarios",
                column: "idRol");

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
                name: "Fotos");

            migrationBuilder.DropTable(
                name: "jwt_Roles");

            migrationBuilder.DropTable(
                name: "Reservaciones");

            migrationBuilder.DropTable(
                name: "Valoraciones");

            migrationBuilder.DropTable(
                name: "CasasPendientes");

            migrationBuilder.DropTable(
                name: "Casas");

            migrationBuilder.DropTable(
                name: "Caracteristicas");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

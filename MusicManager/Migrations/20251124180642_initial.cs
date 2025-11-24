using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicManager.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Etichette",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    SedeLegale = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etichette", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Festival",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    DataInizio = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festival", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cantanti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeArte = table.Column<string>(type: "TEXT", nullable: false),
                    NomeReale = table.Column<string>(type: "TEXT", nullable: false),
                    EtichettaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cantanti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cantanti_Etichette_EtichettaId",
                        column: x => x.EtichettaId,
                        principalTable: "Etichette",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Esibizioni",
                columns: table => new
                {
                    FestivalId = table.Column<int>(type: "INTEGER", nullable: false),
                    CantanteId = table.Column<int>(type: "INTEGER", nullable: false),
                    VotiGiuria = table.Column<int>(type: "INTEGER", nullable: false),
                    OrdineUscita = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Esibizioni", x => new { x.CantanteId, x.FestivalId });
                    table.ForeignKey(
                        name: "FK_Esibizioni_Cantanti_CantanteId",
                        column: x => x.CantanteId,
                        principalTable: "Cantanti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Esibizioni_Festival_FestivalId",
                        column: x => x.FestivalId,
                        principalTable: "Festival",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cantanti_EtichettaId",
                table: "Cantanti",
                column: "EtichettaId");

            migrationBuilder.CreateIndex(
                name: "IX_Esibizioni_FestivalId",
                table: "Esibizioni",
                column: "FestivalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Esibizioni");

            migrationBuilder.DropTable(
                name: "Cantanti");

            migrationBuilder.DropTable(
                name: "Festival");

            migrationBuilder.DropTable(
                name: "Etichette");
        }
    }
}

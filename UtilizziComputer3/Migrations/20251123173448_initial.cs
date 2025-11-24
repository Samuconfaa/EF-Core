using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtilizziComputer3.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Aula = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Computer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Modello = table.Column<string>(type: "TEXT", nullable: false),
                    Collocazione = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cognome = table.Column<string>(type: "TEXT", nullable: false),
                    ClasseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studenti_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilizza",
                columns: table => new
                {
                    StudenteId = table.Column<int>(type: "INTEGER", nullable: false),
                    ComputerId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataOraInizioUtilizzo = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DataOraFineUtilizzo = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizza", x => new { x.StudenteId, x.ComputerId, x.DataOraInizioUtilizzo });
                    table.ForeignKey(
                        name: "FK_Utilizza_Computer_ComputerId",
                        column: x => x.ComputerId,
                        principalTable: "Computer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utilizza_Studenti_StudenteId",
                        column: x => x.StudenteId,
                        principalTable: "Studenti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classe_Nome",
                table: "Classe",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Studenti_ClasseId",
                table: "Studenti",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizza_ComputerId",
                table: "Utilizza",
                column: "ComputerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilizza");

            migrationBuilder.DropTable(
                name: "Computer");

            migrationBuilder.DropTable(
                name: "Studenti");

            migrationBuilder.DropTable(
                name: "Classe");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb1_OOP.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bokning",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    slutDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    maxAntal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bokning", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medlem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    telefonNummer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medlem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    minAntalSpelare = table.Column<int>(type: "int", nullable: false),
                    maxAntalSpelare = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bokning");

            migrationBuilder.DropTable(
                name: "Medlem");

            migrationBuilder.DropTable(
                name: "Spel");
        }
    }
}

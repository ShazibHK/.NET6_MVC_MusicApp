using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorLogger",
                columns: table => new
                {
                    LoggerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorLogger", x => x.LoggerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorLogger");

           
        }
    }
}

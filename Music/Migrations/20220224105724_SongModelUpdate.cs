using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.Migrations
{
    public partial class SongModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Year",
                table: "Song",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Song");
        }
    }
}

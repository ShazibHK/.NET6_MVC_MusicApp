using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.Migrations
{
    public partial class SongPlaylistNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "Song",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Song");
        }
    }
}

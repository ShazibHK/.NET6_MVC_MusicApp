using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.Migrations
{
    public partial class AddImage3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SongPath",
                table: "Song",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongPath",
                table: "Song");
        }
    }
}

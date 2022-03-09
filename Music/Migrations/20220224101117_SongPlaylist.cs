using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.Migrations
{
    public partial class SongPlaylist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayList",
                columns: table => new
                {
                    PlayListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayList", x => x.PlayListId);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongId);
                    table.ForeignKey(
                        name: "FK_Song_PlayList_PlayListId",
                        column: x => x.PlayListId,
                        principalTable: "PlayList",
                        principalColumn: "PlayListId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Song_PlayListId",
                table: "Song",
                column: "PlayListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "PlayList");
        }
    }
}

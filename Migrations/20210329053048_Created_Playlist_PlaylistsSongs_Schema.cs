using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckListApp.Migrations
{
    public partial class Created_Playlist_PlaylistsSongs_Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UserSongAddedDate",
                table: "UsersSongs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaylistTitle = table.Column<string>(maxLength: 50, nullable: false),
                    PlaylistDescription = table.Column<string>(maxLength: 300, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Playlists_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistsSongs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaylistID = table.Column<int>(nullable: false),
                    SongID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistsSongs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PlaylistsSongs_Playlists_PlaylistID",
                        column: x => x.PlaylistID,
                        principalTable: "Playlists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistsSongs_Songs_SongID",
                        column: x => x.SongID,
                        principalTable: "Songs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UserID",
                table: "Playlists",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsSongs_PlaylistID",
                table: "PlaylistsSongs",
                column: "PlaylistID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistsSongs_SongID",
                table: "PlaylistsSongs",
                column: "SongID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistsSongs");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropColumn(
                name: "UserSongAddedDate",
                table: "UsersSongs");
        }
    }
}

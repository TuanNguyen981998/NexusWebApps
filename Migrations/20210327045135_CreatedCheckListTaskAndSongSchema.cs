using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckListApp.Migrations
{
    public partial class CreatedCheckListTaskAndSongSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckListTasks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskDescription = table.Column<string>(maxLength: 500, nullable: false),
                    TaskPriority = table.Column<int>(nullable: false),
                    DateDue = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    TaskStatus = table.Column<bool>(nullable: false),
                    UserID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CheckListTasks_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Artist = table.Column<string>(nullable: false),
                    Duration = table.Column<string>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UsersSongs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    SongID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSongs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UsersSongs_Songs_SongID",
                        column: x => x.SongID,
                        principalTable: "Songs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersSongs_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListTasks_UserID",
                table: "CheckListTasks",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSongs_SongID",
                table: "UsersSongs",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersSongs_UserID",
                table: "UsersSongs",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListTasks");

            migrationBuilder.DropTable(
                name: "UsersSongs");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}

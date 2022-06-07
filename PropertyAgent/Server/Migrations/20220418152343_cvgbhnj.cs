using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyAgent.Server.Migrations
{
    public partial class cvgbhnj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AppUserId",
                table: "Properties",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AspNetUsers_AppUserId",
                table: "Properties",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_AppUserId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AppUserId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Properties");
        }
    }
}

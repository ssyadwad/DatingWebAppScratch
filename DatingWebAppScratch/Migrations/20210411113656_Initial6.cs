using Microsoft.EntityFrameworkCore.Migrations;

namespace DatingWebAppScratch.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockApplication_Users_AppUserId",
                table: "StockApplication");

            migrationBuilder.DropIndex(
                name: "IX_StockApplication_AppUserId",
                table: "StockApplication");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "StockApplication");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StockApplication",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockApplication_UserId",
                table: "StockApplication",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockApplication_Users_UserId",
                table: "StockApplication",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockApplication_Users_UserId",
                table: "StockApplication");

            migrationBuilder.DropIndex(
                name: "IX_StockApplication_UserId",
                table: "StockApplication");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StockApplication");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "StockApplication",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockApplication_AppUserId",
                table: "StockApplication",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockApplication_Users_AppUserId",
                table: "StockApplication",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

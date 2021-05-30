using Microsoft.EntityFrameworkCore.Migrations;

namespace EduPortalV2.Migrations
{
    public partial class EDuMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Educator",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Educator",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Educator_ApplicationUserId",
                table: "Educator",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Educator_AspNetUsers_ApplicationUserId",
                table: "Educator",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Educator_AspNetUsers_ApplicationUserId",
                table: "Educator");

            migrationBuilder.DropIndex(
                name: "IX_Educator_ApplicationUserId",
                table: "Educator");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Educator");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Educator");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace EduPortalV2.Migrations
{
    public partial class EDuMig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyCourse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    EducaterId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Statu = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EducatorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyCourse_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MyCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyCourse_Educator_EducatorId",
                        column: x => x.EducatorId,
                        principalTable: "Educator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyCourse_ApplicationUserId",
                table: "MyCourse",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MyCourse_CourseId",
                table: "MyCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_MyCourse_EducatorId",
                table: "MyCourse",
                column: "EducatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyCourse");
        }
    }
}

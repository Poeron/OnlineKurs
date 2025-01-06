using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineKurs.Migrations
{
    /// <inheritdoc />
    public partial class navigationforcoursesandenrollments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnrollmentsId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EnrollmentsId",
                table: "Courses",
                column: "EnrollmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Enrollments_EnrollmentsId",
                table: "Courses",
                column: "EnrollmentsId",
                principalTable: "Enrollments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Enrollments_EnrollmentsId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_EnrollmentsId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EnrollmentsId",
                table: "Courses");
        }
    }
}

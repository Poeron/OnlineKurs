using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineKurs.Migrations
{
    /// <inheritdoc />
    public partial class navigasyon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Enrollments_EnrollmentsId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Reviews_ReviewsId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_EnrollmentsId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ReviewsId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "EnrollmentsId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ReviewsId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Reviews",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Enrollments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CoursesId",
                table: "Reviews",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CoursesId",
                table: "Enrollments",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CoursesId",
                table: "Enrollments",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Courses_CoursesId",
                table: "Reviews",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CoursesId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Courses_CoursesId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CoursesId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CoursesId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Enrollments");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentsId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewsId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EnrollmentsId",
                table: "Courses",
                column: "EnrollmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ReviewsId",
                table: "Courses",
                column: "ReviewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Enrollments_EnrollmentsId",
                table: "Courses",
                column: "EnrollmentsId",
                principalTable: "Enrollments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Reviews_ReviewsId",
                table: "Courses",
                column: "ReviewsId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }
    }
}

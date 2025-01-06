using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineKurs.Migrations
{
    /// <inheritdoc />
    public partial class anothernavigationforcourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewsId",
                table: "Courses",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ReviewsId",
                table: "Courses",
                column: "ReviewsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Reviews_ReviewsId",
                table: "Courses",
                column: "ReviewsId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Reviews_ReviewsId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ReviewsId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ReviewsId",
                table: "Courses");
        }
    }
}

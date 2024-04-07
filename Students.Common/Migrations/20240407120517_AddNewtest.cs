using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Common.Migrations
{
    /// <inheritdoc />
    public partial class AddNewtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectureHallId",
                table: "Subject",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_LectureHallId",
                table: "Subject",
                column: "LectureHallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_LectureHall_LectureHallId",
                table: "Subject",
                column: "LectureHallId",
                principalTable: "LectureHall",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_LectureHall_LectureHallId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_LectureHallId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "LectureHallId",
                table: "Subject");
        }
    }
}

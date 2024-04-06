using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Common.Migrations
{
    /// <inheritdoc />
    public partial class AddLectureHallMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureHalls",
                table: "LectureHalls");

            migrationBuilder.RenameTable(
                name: "LectureHalls",
                newName: "LectureHall");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureHall",
                table: "LectureHall",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LectureHall",
                table: "LectureHall");

            migrationBuilder.RenameTable(
                name: "LectureHall",
                newName: "LectureHalls");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LectureHalls",
                table: "LectureHalls",
                column: "Id");
        }
    }
}

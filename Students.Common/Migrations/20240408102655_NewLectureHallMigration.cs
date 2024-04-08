using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Common.Migrations
{
    /// <inheritdoc />
    public partial class NewLectureHallMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "LectureHallSubject",
                columns: table => new
                {
                    LectureHallId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureHallSubject", x => new { x.LectureHallId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_LectureHallSubject_LectureHall_LectureHallId",
                        column: x => x.LectureHallId,
                        principalTable: "LectureHall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LectureHallSubject_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LectureHallSubject_SubjectId",
                table: "LectureHallSubject",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LectureHallSubject");

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
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class TeacherFileSubjectTaught : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "TeacherFile",
                columns: table => new
                {
                    TeacherFileId = table.Column<Guid>(nullable: false),
                    Details = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    RecordDate = table.Column<DateTime>(type: "date", nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherFile", x => x.TeacherFileId);
                    table.ForeignKey(
                        name: "FK_TeacherFile_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTaught",
                columns: table => new
                {
                    SubjectTaughtId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTaught", x => x.SubjectTaughtId);
                    table.ForeignKey(
                        name: "FK_SubjectTaught_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTaught_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTaught_SubjectId",
                table: "SubjectTaught",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTaught_TeacherId",
                table: "SubjectTaught",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherFile_TeacherId",
                table: "TeacherFile",
                column: "TeacherId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectTaught");

            migrationBuilder.DropTable(
                name: "TeacherFile");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}

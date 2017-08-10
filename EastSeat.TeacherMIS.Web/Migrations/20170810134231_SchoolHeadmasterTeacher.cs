using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class SchoolHeadmasterTeacher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "School",
                columns: table => new
                {
                    SchoolId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_School", x => x.SchoolId);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    TeacherId = table.Column<Guid>(nullable: false),
                    ConfirmationEscMinute = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CurrentPosition = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CurrentPositionAppMinute = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CurrentPositionPostingDate = table.Column<DateTime>(type: "date", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    FirstAppEscMinute = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    FirstProbationAppDate = table.Column<DateTime>(type: "date", nullable: false),
                    Fullname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Gender = table.Column<string>(type: "char(10)", unicode: false, maxLength: 10, nullable: false),
                    IppsNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ProbationAppDesignation = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    UtsFileNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.TeacherId);
                    table.ForeignKey(
                        name: "FK_Teachers_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Headmaster",
                columns: table => new
                {
                    HeadmasterId = table.Column<Guid>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    SchoolId = table.Column<Guid>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: false),
                    TeacherId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headmaster", x => x.HeadmasterId);
                    table.ForeignKey(
                        name: "FK_Headmaster_School_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "School",
                        principalColumn: "SchoolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Headmaster_Teachers_TeacherId1",
                        column: x => x.TeacherId1,
                        principalTable: "Teachers",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Headmaster_SchoolId",
                table: "Headmaster",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Headmaster_TeacherId1",
                table: "Headmaster",
                column: "TeacherId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Headmaster_TeacherId_SchoolId",
                table: "Headmaster",
                columns: new[] { "TeacherId", "SchoolId" });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_SchoolId",
                table: "Teachers",
                column: "SchoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Headmaster");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "School");
        }
    }
}

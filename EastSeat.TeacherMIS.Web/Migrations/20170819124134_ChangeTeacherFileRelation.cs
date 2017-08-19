using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class ChangeTeacherFileRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherFile_TeacherId",
                table: "TeacherFile");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherFile_TeacherId",
                table: "TeacherFile",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TeacherFile_TeacherId",
                table: "TeacherFile");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherFile_TeacherId",
                table: "TeacherFile",
                column: "TeacherId",
                unique: true);
        }
    }
}

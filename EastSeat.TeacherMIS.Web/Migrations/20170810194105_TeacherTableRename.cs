using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class TeacherTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headmaster_Table_TeacherId",
                table: "Headmaster");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTaught_Table_TeacherId",
                table: "SubjectTaught");

            migrationBuilder.DropForeignKey(
                name: "FK_Table_School_SchoolId",
                table: "Table");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherFile_Table_TeacherId",
                table: "TeacherFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Table",
                table: "Table");

            migrationBuilder.RenameTable(
                name: "Table",
                newName: "Teacher");

            migrationBuilder.RenameIndex(
                name: "IX_Table_SchoolId",
                table: "Teacher",
                newName: "IX_Teacher_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headmaster_Teacher_TeacherId",
                table: "Headmaster",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTaught_Teacher_TeacherId",
                table: "SubjectTaught",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_School_SchoolId",
                table: "Teacher",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherFile_Teacher_TeacherId",
                table: "TeacherFile",
                column: "TeacherId",
                principalTable: "Teacher",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headmaster_Teacher_TeacherId",
                table: "Headmaster");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTaught_Teacher_TeacherId",
                table: "SubjectTaught");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_School_SchoolId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherFile_Teacher_TeacherId",
                table: "TeacherFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teacher",
                table: "Teacher");

            migrationBuilder.RenameTable(
                name: "Teacher",
                newName: "Table");

            migrationBuilder.RenameIndex(
                name: "IX_Teacher_SchoolId",
                table: "Table",
                newName: "IX_Table_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Table",
                table: "Table",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headmaster_Table_TeacherId",
                table: "Headmaster",
                column: "TeacherId",
                principalTable: "Table",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTaught_Table_TeacherId",
                table: "SubjectTaught",
                column: "TeacherId",
                principalTable: "Table",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Table_School_SchoolId",
                table: "Table",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherFile_Table_TeacherId",
                table: "TeacherFile",
                column: "TeacherId",
                principalTable: "Table",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

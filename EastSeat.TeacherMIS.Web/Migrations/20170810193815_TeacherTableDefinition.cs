using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class TeacherTableDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headmaster_Teachers_TeacherId",
                table: "Headmaster");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectTaught_Teachers_TeacherId",
                table: "SubjectTaught");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_School_SchoolId",
                table: "Teachers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherFile_Teachers_TeacherId",
                table: "TeacherFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers");

            migrationBuilder.RenameTable(
                name: "Teachers",
                newName: "Table");

            migrationBuilder.RenameIndex(
                name: "IX_Teachers_SchoolId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "Teachers");

            migrationBuilder.RenameIndex(
                name: "IX_Table_SchoolId",
                table: "Teachers",
                newName: "IX_Teachers_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teachers",
                table: "Teachers",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Headmaster_Teachers_TeacherId",
                table: "Headmaster",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectTaught_Teachers_TeacherId",
                table: "SubjectTaught",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_School_SchoolId",
                table: "Teachers",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "SchoolId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherFile_Teachers_TeacherId",
                table: "TeacherFile",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

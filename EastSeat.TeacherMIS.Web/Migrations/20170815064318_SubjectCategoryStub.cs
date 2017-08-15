using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class SubjectCategoryStub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stub",
                table: "SubjectCategory",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectCategory_Stub",
                table: "SubjectCategory",
                column: "Stub");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubjectCategory_Stub",
                table: "SubjectCategory");

            migrationBuilder.DropColumn(
                name: "Stub",
                table: "SubjectCategory");
        }
    }
}

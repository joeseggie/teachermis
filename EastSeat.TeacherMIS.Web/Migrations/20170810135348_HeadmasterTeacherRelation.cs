using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class HeadmasterTeacherRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headmaster_Teachers_TeacherId1",
                table: "Headmaster");

            migrationBuilder.DropIndex(
                name: "IX_Headmaster_TeacherId1",
                table: "Headmaster");

            migrationBuilder.DropColumn(
                name: "TeacherId1",
                table: "Headmaster");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Headmaster",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Headmaster_TeacherId",
                table: "Headmaster",
                column: "TeacherId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Headmaster_Teachers_TeacherId",
                table: "Headmaster",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Headmaster_Teachers_TeacherId",
                table: "Headmaster");

            migrationBuilder.DropIndex(
                name: "IX_Headmaster_TeacherId",
                table: "Headmaster");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Headmaster",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId1",
                table: "Headmaster",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Headmaster_TeacherId1",
                table: "Headmaster",
                column: "TeacherId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Headmaster_Teachers_TeacherId1",
                table: "Headmaster",
                column: "TeacherId1",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class TeacherGradeAndPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPosition",
                table: "Teacher");

            migrationBuilder.AddColumn<Guid>(
                name: "GradeId",
                table: "Teacher",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionId",
                table: "Teacher",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    GradeId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                columns: table => new
                {
                    PositionId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.PositionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_GradeId",
                table: "Teacher",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_PositionId",
                table: "Teacher",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Grade_GradeId",
                table: "Teacher",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teacher_Position_PositionId",
                table: "Teacher",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "PositionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Grade_GradeId",
                table: "Teacher");

            migrationBuilder.DropForeignKey(
                name: "FK_Teacher_Position_PositionId",
                table: "Teacher");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_GradeId",
                table: "Teacher");

            migrationBuilder.DropIndex(
                name: "IX_Teacher_PositionId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Teacher");

            migrationBuilder.AddColumn<string>(
                name: "CurrentPosition",
                table: "Teacher",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

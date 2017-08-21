using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class SchoolCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SchoolCategoryId",
                table: "School",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SchoolCategory",
                columns: table => new
                {
                    SchoolCategoryId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RowVersion = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCategory", x => x.SchoolCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_School_SchoolCategoryId",
                table: "School",
                column: "SchoolCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_School_SchoolCategory_SchoolCategoryId",
                table: "School",
                column: "SchoolCategoryId",
                principalTable: "SchoolCategory",
                principalColumn: "SchoolCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_SchoolCategory_SchoolCategoryId",
                table: "School");

            migrationBuilder.DropTable(
                name: "SchoolCategory");

            migrationBuilder.DropIndex(
                name: "IX_School_SchoolCategoryId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "SchoolCategoryId",
                table: "School");
        }
    }
}

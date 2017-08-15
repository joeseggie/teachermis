using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class SubjectCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubjectCategoryId",
                table: "Subject",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubjectCategory",
                columns: table => new
                {
                    SubjectCategoryId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectCategory", x => x.SubjectCategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SubjectCategoryId",
                table: "Subject",
                column: "SubjectCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_SubjectCategory_SubjectCategoryId",
                table: "Subject",
                column: "SubjectCategoryId",
                principalTable: "SubjectCategory",
                principalColumn: "SubjectCategoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_SubjectCategory_SubjectCategoryId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "SubjectCategory");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SubjectCategoryId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "SubjectCategoryId",
                table: "Subject");
        }
    }
}

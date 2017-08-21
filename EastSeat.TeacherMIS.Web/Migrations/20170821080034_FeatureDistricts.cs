using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EastSeat.TeacherMIS.Web.Migrations
{
    public partial class FeatureDistricts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DistrictId",
                table: "School",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_School_DistrictId",
                table: "School",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_School_District_DistrictId",
                table: "School",
                column: "DistrictId",
                principalTable: "District",
                principalColumn: "DistrictId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_School_District_DistrictId",
                table: "School");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropIndex(
                name: "IX_School_DistrictId",
                table: "School");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "School");
        }
    }
}

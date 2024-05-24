using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateGuidAllEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Branchs_BranchId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BranchId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BranchId1",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BranchId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BranchId1",
                table: "AspNetUsers",
                column: "BranchId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Branchs_BranchId1",
                table: "AspNetUsers",
                column: "BranchId1",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

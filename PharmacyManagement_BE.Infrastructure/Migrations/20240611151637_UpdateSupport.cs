using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupports_Support_SupportId",
                table: "ProductSupports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Support",
                table: "Support");

            migrationBuilder.RenameTable(
                name: "Support",
                newName: "Supports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supports",
                table: "Supports",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupports_Supports_SupportId",
                table: "ProductSupports",
                column: "SupportId",
                principalTable: "Supports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSupports_Supports_SupportId",
                table: "ProductSupports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supports",
                table: "Supports");

            migrationBuilder.RenameTable(
                name: "Supports",
                newName: "Support");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Support",
                table: "Support",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSupports_Support_SupportId",
                table: "ProductSupports",
                column: "SupportId",
                principalTable: "Support",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

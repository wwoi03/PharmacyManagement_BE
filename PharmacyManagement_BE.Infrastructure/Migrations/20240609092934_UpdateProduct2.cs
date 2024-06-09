using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateProduct2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUnits",
                table: "ProductUnits");

            migrationBuilder.DropIndex(
                name: "IX_ProductUnits_ShipmentDetailsId",
                table: "ProductUnits");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductUnits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUnits",
                table: "ProductUnits",
                columns: new[] { "ShipmentDetailsId", "UnitId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUnits",
                table: "ProductUnits");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "ProductUnits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUnits",
                table: "ProductUnits",
                columns: new[] { "ProductId", "UnitId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_ShipmentDetailsId",
                table: "ProductUnits",
                column: "ShipmentDetailsId");
        }
    }
}

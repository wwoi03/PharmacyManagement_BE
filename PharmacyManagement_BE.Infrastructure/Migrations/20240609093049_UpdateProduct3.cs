using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateProduct3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnits_ShipmentDetails_ShipmentDetailsId",
                table: "ProductUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductUnits_Units_UnitId",
                table: "ProductUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductUnits",
                table: "ProductUnits");

            migrationBuilder.RenameTable(
                name: "ProductUnits",
                newName: "ShipmentDetailsUnit");

            migrationBuilder.RenameIndex(
                name: "IX_ProductUnits_UnitId",
                table: "ShipmentDetailsUnit",
                newName: "IX_ShipmentDetailsUnit_UnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipmentDetailsUnit",
                table: "ShipmentDetailsUnit",
                columns: new[] { "ShipmentDetailsId", "UnitId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentDetailsUnit_ShipmentDetails_ShipmentDetailsId",
                table: "ShipmentDetailsUnit",
                column: "ShipmentDetailsId",
                principalTable: "ShipmentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentDetailsUnit_Units_UnitId",
                table: "ShipmentDetailsUnit",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentDetailsUnit_ShipmentDetails_ShipmentDetailsId",
                table: "ShipmentDetailsUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentDetailsUnit_Units_UnitId",
                table: "ShipmentDetailsUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipmentDetailsUnit",
                table: "ShipmentDetailsUnit");

            migrationBuilder.RenameTable(
                name: "ShipmentDetailsUnit",
                newName: "ProductUnits");

            migrationBuilder.RenameIndex(
                name: "IX_ShipmentDetailsUnit_UnitId",
                table: "ProductUnits",
                newName: "IX_ProductUnits_UnitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductUnits",
                table: "ProductUnits",
                columns: new[] { "ShipmentDetailsId", "UnitId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnits_ShipmentDetails_ShipmentDetailsId",
                table: "ProductUnits",
                column: "ShipmentDetailsId",
                principalTable: "ShipmentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductUnits_Units_UnitId",
                table: "ProductUnits",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

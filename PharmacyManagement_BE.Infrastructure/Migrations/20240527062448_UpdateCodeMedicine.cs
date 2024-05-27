using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateCodeMedicine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoldPrice",
                table: "ShipmentDetails");

            migrationBuilder.AddColumn<string>(
                name: "ProductionBatch",
                table: "ShipmentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sold",
                table: "ShipmentDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionBatch",
                table: "ShipmentDetails");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "ShipmentDetails");

            migrationBuilder.AddColumn<decimal>(
                name: "SoldPrice",
                table: "ShipmentDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateShipmentUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "ShipmentDetailsUnit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitCount",
                table: "ShipmentDetailsUnit",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "ShipmentDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "ShipmentDetailsUnit");

            migrationBuilder.DropColumn(
                name: "UnitCount",
                table: "ShipmentDetailsUnit");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ShipmentDetails");
        }
    }
}

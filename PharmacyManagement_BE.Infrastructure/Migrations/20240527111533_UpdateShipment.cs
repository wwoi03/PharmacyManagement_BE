using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_AspNetUsers_StaffId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_StaffId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Suppliers");

            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "Shipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_StaffId",
                table: "Shipments",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_AspNetUsers_StaffId",
                table: "Shipments",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_AspNetUsers_StaffId",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_StaffId",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Shipments");

            migrationBuilder.AddColumn<Guid>(
                name: "StaffId",
                table: "Suppliers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_StaffId",
                table: "Suppliers",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_AspNetUsers_StaffId",
                table: "Suppliers",
                column: "StaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

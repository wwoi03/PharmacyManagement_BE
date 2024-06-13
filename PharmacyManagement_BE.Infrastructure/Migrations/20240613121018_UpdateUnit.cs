using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyManagement_BE.Infrastructure.Migrations
{
    public partial class UpdateUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "UnitType",
                table: "Units",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "ProductIngredients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_UnitId",
                table: "ProductIngredients",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductIngredients_Units_UnitId",
                table: "ProductIngredients",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductIngredients_Units_UnitId",
                table: "ProductIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ProductIngredients_UnitId",
                table: "ProductIngredients");

            migrationBuilder.DropColumn(
                name: "UnitType",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "ProductIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Ingredients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsInStorage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GateId",
                table: "Trips",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "PalletsCount",
                table: "Trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Storages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OccupancyActual",
                table: "Storages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OccupancyExpected",
                table: "Storages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_GateId",
                table: "Trips",
                column: "GateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Gates_GateId",
                table: "Trips",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Gates_GateId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_GateId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "GateId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "PalletsCount",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "OccupancyActual",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "OccupancyExpected",
                table: "Storages");
        }
    }
}

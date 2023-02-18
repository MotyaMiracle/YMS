using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_Drivers_DriverId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_DriverId",
                table: "Routes");

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "ArrivalTime", "DriverId", "NowStatus", "StorageId" },
                values: new object[] { new Guid("6d72a696-17db-4020-b0ae-88c1c86e701e"), new DateTime(2022, 2, 18, 12, 0, 0, 0, DateTimeKind.Utc), new Guid("2c96f974-e8d2-48f9-8040-bb649bf41aaf"), 0, new Guid("406c5d8b-00ee-419a-b901-4d670601ee4f") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: new Guid("6d72a696-17db-4020-b0ae-88c1c86e701e"));

            migrationBuilder.CreateIndex(
                name: "IX_Routes_DriverId",
                table: "Routes",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_Drivers_DriverId",
                table: "Routes",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

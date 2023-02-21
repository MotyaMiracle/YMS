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
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageId = table.Column<Guid>(type: "uuid", nullable: false),
                    DriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NowStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "ArrivalTime", "DriverId", "NowStatus", "StorageId" },
                values: new object[] { new Guid("a42490c8-d837-4613-b28a-aac251903e45"), new DateTime(2022, 2, 18, 16, 0, 0, 0, DateTimeKind.Utc), new Guid("564afa54-2f20-4521-85f5-bb0e080d7f2a"), 2, new Guid("33066b28-d708-46d8-a683-c2ae0dcd2f44") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}

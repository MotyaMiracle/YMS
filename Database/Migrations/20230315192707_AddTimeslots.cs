using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeslots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "TimeslotId",
                table: "Trips",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Timeslots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    From = table.Column<string>(type: "text", nullable: false),
                    To = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeslots", x => x.Id);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Trips_TimeslotId",
                table: "Trips",
                column: "TimeslotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timeslots");


            migrationBuilder.DropIndex(
                name: "IX_Trips_TimeslotId",
                table: "Trips");


            migrationBuilder.DropColumn(
                name: "TimeslotId",
                table: "Trips");
        }
    }
}

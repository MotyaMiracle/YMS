using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddColorStatusToTruck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trips_TimeslotId",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "ColorStatus",
                table: "Trucks",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorStatus",
                table: "Trucks");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TimeslotId",
                table: "Trips",
                column: "TimeslotId");
        }
    }
}

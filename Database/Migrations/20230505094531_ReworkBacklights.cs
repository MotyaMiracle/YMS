using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ReworkBacklights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backlight",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "ColorStatus",
                table: "Trucks");

            migrationBuilder.AddColumn<string>(
                name: "Backlights",
                table: "Trips",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backlights",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "Backlight",
                table: "Trucks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColorStatus",
                table: "Trucks",
                type: "integer",
                nullable: true);
        }
    }
}

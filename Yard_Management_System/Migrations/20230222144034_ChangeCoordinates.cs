using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinates",
                table: "Storages");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Storages",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Storages",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Storages");

            migrationBuilder.AddColumn<double[]>(
                name: "Coordinates",
                table: "Storages",
                type: "double precision[]",
                nullable: false,
                defaultValue: new double[0]);
        }
    }
}

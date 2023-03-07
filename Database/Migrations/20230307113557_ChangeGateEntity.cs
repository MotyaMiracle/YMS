using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpeningHours",
                table: "Gates");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ClosingHour",
                table: "Gates",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "OpeningHour",
                table: "Gates",
                type: "time without time zone",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingHour",
                table: "Gates");

            migrationBuilder.DropColumn(
                name: "OpeningHour",
                table: "Gates");

            migrationBuilder.AddColumn<string>(
                name: "OpeningHours",
                table: "Gates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

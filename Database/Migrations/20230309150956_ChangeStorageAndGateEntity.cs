using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangeStorageAndGateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("f360f334-25c7-424d-827b-7607f67931ba"));

            migrationBuilder.RenameColumn(
                name: "CarNumber",
                table: "Trucks",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "CarBrand",
                table: "Trucks",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "OpeningHours",
                table: "Storages",
                newName: "OpeningHour");

            migrationBuilder.AddColumn<string>(
                name: "ClosingHour",
                table: "Storages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "OpeningHour",
                table: "Gates",
                type: "text",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "ClosingHour",
                table: "Gates",
                type: "text",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time without time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingHour",
                table: "Storages");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Trucks",
                newName: "CarNumber");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "Trucks",
                newName: "CarBrand");

            migrationBuilder.RenameColumn(
                name: "OpeningHour",
                table: "Storages",
                newName: "OpeningHours");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "OpeningHour",
                table: "Gates",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "ClosingHour",
                table: "Gates",
                type: "time without time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Companies",
                column: "Id",
                value: new Guid("f360f334-25c7-424d-827b-7607f67931ba"));
        }
    }
}

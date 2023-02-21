using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoryEntryField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EntityId",
                table: "HistoryEntries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "HistoryEntries");
        }
    }
}

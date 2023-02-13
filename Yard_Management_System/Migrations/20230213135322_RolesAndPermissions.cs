using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RolesAndPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ListOfPermissions = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ListOfPermissions", "Name" },
                values: new object[,]
                {
                    { new Guid("624df587-97b9-4b82-a025-c1ebfdfed153"), new[] { 2, 0 }, "Оператор стойки регистрации" },
                    { new Guid("a2524daf-6498-471a-adf8-f6c54703cf08"), new[] { 2, 1, 3, 0 }, "Гл. Администратор" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

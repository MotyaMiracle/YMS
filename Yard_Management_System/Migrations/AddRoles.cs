using Microsoft.EntityFrameworkCore.Migrations;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Migrations
{
    public partial class AddRoles : Migration
    {
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
                    { GenerationRoleId.ReceptionistId, new[] { 2, 0 }, "Оператор стойки регистрации" },
                    { GenerationRoleId.AdminId, new[] { 2, 1, 3, 0 }, "Гл. Администратор" }
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

using Microsoft.EntityFrameworkCore.Migrations;

namespace Yard_Management_System.Migrations
{
    public partial class AddUsersInDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { GenerateIdRoles.userId, "User" },
                     { GenerateIdRoles.adminId, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Login", "Password", "PasswordHash", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("5dd30af3-7fd4-4793-b777-2f48205138d0"), "tom@gmail.com", false, "tom123", "12345", null, "89169436523", GenerateIdRoles.adminId },
                    { new Guid("b58b787e-bb63-4649-9809-0ed1604e4efb"), "alice@gmail.com", false, "alice321", "54321", null, "89267434513", GenerateIdRoles.userId }
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

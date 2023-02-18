using Microsoft.EntityFrameworkCore.Migrations;
using Yard_Management_System.Entity;

#nullable disable

namespace YardManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class InsertUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ListOfPermissions", "Name" },
                values: new object[,]
                {
                    { GenerationRoleId.ReceptionistId, new[] { 2, 0 }, "Оператор стойки регистрации" },
                    { GenerationRoleId.AdminId, new[] { 2, 1, 3, 0 }, "Гл. Администратор" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Login", "Password", "PasswordHash", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("5dd30af3-7fd4-4793-b777-2f48205138d0"), "tom@gmail.com", false, "tom123", "12345", null, "89169436523", GenerationRoleId.AdminId},
                    { new Guid("b58b787e-bb63-4649-9809-0ed1604e4efb"), "alice@gmail.com", false, "alice321", "54321", null, "89267434513", GenerationRoleId.ReceptionistId }
                });    
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

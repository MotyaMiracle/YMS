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
                    { SystemRoleIds.ReceptionistId, new[] { 2, 0 }, "Оператор стойки регистрации" },
                    { SystemRoleIds.AdminId, new[] { 2, 1, 3, 0 }, "Гл. Администратор" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsActive", "Login", "PasswordHash", "PhoneNumber", "RoleId" },
                values: new object[,]
                {
                    { new Guid("ee976536-0900-4a50-a530-ff76ffd77302"), "123@gmail.com", false, "123", "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=", "123", SystemRoleIds.AdminId},
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

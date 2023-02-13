using Microsoft.EntityFrameworkCore;
using System.Data;
using Yard_Management_System.Entity;

namespace Yard_Management_System
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Role MainAdmin = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Гл. Администратор",
                ListOfPermissions = new List<Permissions> {
                    Permissions.Delete,
                    Permissions.Create,
                    Permissions.Update,
                    Permissions.Read
                }
            };

            Role Receptionist = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Оператор стойки регистрации",
                ListOfPermissions = new List<Permissions> {
                    Permissions.Delete,
                    Permissions.Read
                }
            };

            modelBuilder.Entity<Role>().HasData(MainAdmin, Receptionist);
        }

    }
}

using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.IdentityModel.Tokens.Jwt;
using Serilog;
using System.ComponentModel.DataAnnotations.Schema;
using Yard_Management_System.Models;
using System.Data;

namespace Yard_Management_System
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => new { u.Login, u.Email }).IsUnique();

            Role admin = new Role { Id = Guid.NewGuid(), Name = "Admin" };
            Role user = new Role { Id = Guid.NewGuid(), Name = "User" };
            modelBuilder.Entity<Role>().HasData(admin, user);

            User tom = new User
            {
                Id = Guid.NewGuid(),
                Login = "tom123",
                Email = "tom@gmail.com",
                Password = "12345",
                IsActive = false,
                PhoneNumber = "89169436523",
                RoleId = admin.Id
            };
            User alice = new User
            {
                Id = Guid.NewGuid(),
                Login = "alice321",
                Email = "alice@gmail.com",
                Password = "54321",
                IsActive = false,
                PhoneNumber = "89267434513",
                RoleId = user.Id
            };
            modelBuilder.Entity<User>().HasData(tom, alice);
        }
    }
}
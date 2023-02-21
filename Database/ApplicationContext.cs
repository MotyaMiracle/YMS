using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Yard_Management_System
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<MyFile> Files { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<HistoryEntry> HistoryEntries { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => new { u.Login, u.Email }).IsUnique();
        }
    }
}
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<EntityFile> Files { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<HistoryEntry> HistoryEntries { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Company> Companies { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => new { u.Login, u.Email }).IsUnique();
            modelBuilder.Entity<Driver>().HasIndex(d => new { d.Passport, d.DriveLicense }).IsUnique();
            modelBuilder.Entity<Storage>().HasIndex(s => s.Name).IsUnique();

            modelBuilder.Entity<Company>().HasData(
                new Company { Id = new Guid("f360f334-25c7-424d-827b-7607f67931ba") }
                );
        }
    }
}
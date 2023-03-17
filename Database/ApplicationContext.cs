using Microsoft.EntityFrameworkCore;
using Domain.Entity;


namespace Database
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
        public DbSet<Gate> Gates { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => new { u.Login, u.Email }).IsUnique();
            modelBuilder.Entity<Driver>().HasIndex(d => new { d.Passport, d.DriveLicense }).IsUnique();
            modelBuilder.Entity<Storage>().HasIndex(s => s.Name).IsUnique();
            modelBuilder.Entity<Trip>()
                            .HasOne(t => t.Timeslot)
                            .WithOne(t => t.Trip)
                            .HasForeignKey<Timeslot>(t => t.TripId);
        }
    }
}
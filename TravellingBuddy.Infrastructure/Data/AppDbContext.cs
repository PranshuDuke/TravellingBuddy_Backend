using Microsoft.EntityFrameworkCore;
using TravellingBuddy.Domain.Entities;

namespace TravellingBuddy.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Ride> Rides => Set<Ride>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<City> Cities => Set<City>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 👇 We'll configure relationships here
        ConfigureRide(modelBuilder);
        ConfigureBooking(modelBuilder);
    }

    private void ConfigureRide(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ride>()
            .HasOne(r => r.Driver)
            .WithMany(u => u.RidesOffered)
            .HasForeignKey(r => r.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ride>()
            .HasOne(r => r.FromCity)
            .WithMany(c => c.FromRides)
            .HasForeignKey(r => r.FromCityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ride>()
            .HasOne(r => r.ToCity)
            .WithMany(c => c.ToRides)
            .HasForeignKey(r => r.ToCityId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private void ConfigureBooking(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Ride)
            .WithMany(r => r.Bookings)
            .HasForeignKey(b => b.RideId);

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Passenger)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.PassengerId);
    }
}

using TravellingBuddy.Domain.Enums;

namespace TravellingBuddy.Domain.Entities;

public class Ride
{
    public Guid Id { get; set; }

    public Guid DriverId { get; set; }

    public int FromCityId { get; set; }

    public int ToCityId { get; set; }

    public DateTime DepartureTime { get; set; }

    public int AvailableSeats { get; set; }

    public decimal PricePerSeat { get; set; }

    public DateTime CreatedAt { get; set; }

    public RideStatus Status { get; set; }

    // Navigation Properties
    public User? Driver { get; set; }

    public City? FromCity { get; set; }

    public City? ToCity { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}
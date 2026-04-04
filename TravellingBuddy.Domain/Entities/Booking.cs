using TravellingBuddy.Domain.Enums;

namespace TravellingBuddy.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }

    public Guid RideId { get; set; }

    public Guid PassengerId { get; set; }

    public int SeatsBooked { get; set; }

    public BookingStatus Status { get; set; }

    public DateTime BookedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Ride? Ride { get; set; }

    public User? Passenger { get; set; }
}
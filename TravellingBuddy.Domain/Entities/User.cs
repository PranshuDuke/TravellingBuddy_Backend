namespace TravellingBuddy.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsVerified { get; set; }

    // Navigation Properties
    public ICollection<Ride> RidesOffered { get; set; } = new HashSet<Ride>();

    public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}
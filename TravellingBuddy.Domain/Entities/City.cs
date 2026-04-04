namespace TravellingBuddy.Domain.Entities;

public class City
{
    public int Id { get; set; }

    public string CityName { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    // Navigation
    public ICollection<Ride> FromRides { get; set; } = new HashSet<Ride>();

    public ICollection<Ride> ToRides { get; set; } = new HashSet<Ride>();
}
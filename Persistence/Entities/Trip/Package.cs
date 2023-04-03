namespace PremiumTravelService.Api.Persistence.Entities.Trip;

public class Package
{
    public Guid TripDetailId { get; set; }
    public DateTimeOffset TripStart { get; set; }
    public DateTimeOffset TripEnd { get; set; }
    public string Description { get; set; }
    public decimal Total { get; set; }
}
namespace PremiumTravelService.Api.Persistence.Entities.Trip;

public class Itinerary
{
    public Guid IteneraryId { get; set; }
    public DateTimeOffset TripStart { get; set; }
    public DateTimeOffset TripEnd { get; set; }
    public Package[] TripDetails { get; set; }
}
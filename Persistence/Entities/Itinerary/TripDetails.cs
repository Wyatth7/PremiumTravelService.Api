namespace PremiumTravelService.Api.Persistence.Entities.Itinerary;

public class TripDetails
{
    public DateTimeOffset TripStart { get; set; }
    public DateTimeOffset TripEnd { get; set; }
    public string Description { get; set; }
}
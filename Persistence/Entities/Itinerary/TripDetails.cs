namespace PremiumTravelService.Api.Persistence.Entities.Itinerary;

public class TripDetails
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public string Description { get; set; }
}
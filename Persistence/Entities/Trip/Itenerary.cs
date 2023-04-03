namespace PremiumTravelService.Api.Persistence.Entities.Trip;

public class Itenerary
{
    public Guid IteneraryId { get; set; }
    public DateTimeOffset TripStart { get; set; }
    public DateTimeOffset TripEnd { get; set; }
    public TripDetail[] TripDetails { get; set; }
}
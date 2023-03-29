namespace PremiumTravelService.Api.Persistence.Entities;

public class Itenerary
{
    public Guid IteneraryId { get; set; }
    public DateTimeOffset TripStart { get; set; }
    public DateTimeOffset TripEnd { get; set; }
    public IEnumerable<TripDetail> TripDetails { get; set; }
}
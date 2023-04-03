namespace PremiumTravelService.Api.Persistence.Entities.Trip;

public class TripDetail
{
    public Guid TripDetailId { get; set; }
    public DateTimeOffset TripStart { get; set; }
    public DateTimeOffset TripEnd { get; set; }
    public string Description { get; set; }
}
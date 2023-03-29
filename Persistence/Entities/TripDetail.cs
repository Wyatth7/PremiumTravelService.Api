namespace PremiumTravelService.Api.Persistence.Entities;

public class TripDetail
{
    public Guid TripDetailId { get; set; }
    public DateTimeOffset TripStart { get; set; }
    public DateTimeOffset TripEnd { get; set; }
    public string Description { get; set; }
}
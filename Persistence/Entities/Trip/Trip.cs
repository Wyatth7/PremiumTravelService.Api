using PremiumTravelService.Api.Persistence.Entities.Trip.Bills;

namespace PremiumTravelService.Api.Persistence.Entities.Trip;

public class Trip
{
    public Guid TripId { get; set; }
    public Person[] Travellers { get; set; }
    public Bill Bill { get; set; }
    public Itenerary Itenerary { get; set; }
    public string ThankYouNote { get; set; }
}
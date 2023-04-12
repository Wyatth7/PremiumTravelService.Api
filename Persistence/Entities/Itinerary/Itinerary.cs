using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Persistence.Entities.Itinerary;

public class Itinerary
{
    public string[] Travellers { get; set; }
    public string[] TripDetails { get; set; }
    public ItineraryBilling Billing { get; set; }
    public string ThankYouNote { get; set; }
}
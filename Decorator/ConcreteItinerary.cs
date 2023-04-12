using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator;

public class ConcreteItinerary : ItineraryBase
{
    public ConcreteItinerary()
    {
    }

    public override async Task<Itinerary> PopulateItinerary()
    {
        base.Itinerary.ThankYouNote = "test note";
        return base.Itinerary;
    }

}
using PremiumTravelService.Api.Persistence.Entities.Itinerary;

namespace PremiumTravelService.Api.Decorator;

public abstract class ItineraryBase : IItinerary
{

    public Persistence.Entities.Itinerary.Itinerary Itinerary = new Itinerary();
    public abstract Task<Itinerary> PopulateItinerary();
}
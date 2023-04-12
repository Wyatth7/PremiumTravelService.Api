using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator.Decorators;

public class TravellerDecorator : ItineraryDecorator
{
    public TravellerDecorator(ItineraryBase itineraryBase) : base(itineraryBase)
    {
        
    }
    
    public async Task<Itinerary> PopulateItinerary()
    {
        base.ItineraryBase.Itinerary.Travellers = new[] { "traveller 1" };
        return await base.PopulateItinerary();
    }
}
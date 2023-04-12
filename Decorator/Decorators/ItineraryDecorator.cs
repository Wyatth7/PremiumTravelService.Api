using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator.Decorators;

public class ItineraryDecorator : ItineraryBase
{
    protected ItineraryBase ItineraryBase;

    public ItineraryDecorator(ItineraryBase itineraryBase)
    {
        ItineraryBase = itineraryBase;
    }
    
    public override async Task<Itinerary> PopulateItinerary()
    {
        if (ItineraryBase is not null)
        {
            return await ItineraryBase.PopulateItinerary();
        }
        else
        {
            return new Itinerary();
        }
    }
}
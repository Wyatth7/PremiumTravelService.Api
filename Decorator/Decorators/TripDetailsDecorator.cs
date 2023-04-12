using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator.Decorators;

public class TripDetailsDecorator : ItineraryDecorator
{
    public TripDetailsDecorator(ItineraryBase itineraryBase) : base(itineraryBase)
    {
        
    }
    
    public override async Task<Itinerary> PopulateItinerary(Trip trip, Itinerary itinerary)
    {
        var tripDetails = trip.Packages
            .OrderBy(p => p.TripStart);

        // add start and end times
        itinerary.StartDate = tripDetails.First().TripStart;
        itinerary.EndDate = tripDetails.Last().TripEnd;
        
        // format details
        var details = new List<TripDetails>();
        foreach (var detail in tripDetails)
        {
            details.Add(new()
            {
                TripStart = detail.TripStart,
                TripEnd = detail.TripEnd,
                Description = detail.Description
            });
        }
        
        itinerary.TripDetails = details.ToArray();
        return await base.PopulateItinerary(trip, itinerary);
    }
}
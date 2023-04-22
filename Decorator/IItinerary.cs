using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Decorator;

public interface IItinerary
{
    /// <summary>
    /// Populates an itinerary with specific data
    /// </summary>
    /// <param name="trip">Trip data</param>
    /// <param name="tripDetails">itinerary to add data to</param>
    /// <returns>An itinerary</returns>
    Task<Itinerary> PopulateItinerary(Trip trip, Itinerary tripDetails);
}
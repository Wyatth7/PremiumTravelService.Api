using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.StateMachine.States;

/// <summary>
/// State interface
/// </summary>
public interface IState
{
    /// <summary>
    /// Runs state process
    /// </summary>
    /// <param name="trip">Trip to add payload to</param>
    /// <param name="payload">Data to add to trip</param>
    /// <returns>Updated trip</returns>
    Trip Process(Trip trip, object payload);
    // Task<Trip> ProcessAsync(Trip trip, object payload);
}
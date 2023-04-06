using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.StateMachine;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Services.DataStorage;

/// <summary>
/// Service for easily handling data storage
/// </summary>
public interface IDataStorageService
{
    /// <summary>
    /// Writes to file with instance of <see cref="FileHandler"/>
    /// </summary>
    /// <param name="payload">Data to write to file</param>
    Task Write(StorageData payload);
    /// <summary>
    /// Reads data with instance of <see cref="FileHandler"/>
    /// </summary>
    /// <returns>Storage data object</returns>
    Task<StorageData> Read();

    /// <summary>
    /// Fetches specific trip from storage
    /// </summary>
    /// <param name="tripId">Id of trip to fetch</param>
    /// <returns>Trip or null</returns>
    Task<Trip> FetchTrip(Guid tripId);

    /// <summary>
    /// Fetches specific trip state from storage
    /// </summary>
    /// <param name="tripId">Id of trip state to fetch</param>
    /// <returns>trip state or null</returns>
    Task<TripState> FetchTripState(Guid tripId);
}
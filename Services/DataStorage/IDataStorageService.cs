using PremiumTravelService.Api.Persistence.Entities;
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
}
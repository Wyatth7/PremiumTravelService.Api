using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Extensions.Options;
using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Factory;
using PremiumTravelService.Api.Models.Options;
using PremiumTravelService.Api.Persistence.Entities;
using PremiumTravelService.Api.Persistence.Entities.StateMachine;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Services.DataStorage;

public class DataStorageService : IDataStorageService
{
    private readonly DataStorageOptions _dataStorageOptions;
    private readonly string _path;
    private readonly StorageFactory _storageFactory = new();
    private readonly FileHandler _fileHandler;

    public DataStorageService(IOptionsSnapshot<DataStorageOptions> options)
    {
        _dataStorageOptions = options.Value;
        _path = Path.GetFullPath(_dataStorageOptions.FilePath);
        _fileHandler = _storageFactory.CreateFileHandler(_dataStorageOptions.FileType);
    }

    public async Task Write(StorageData payload)
    {
        await _fileHandler.Write(payload, _path);
    }
    
    public async Task<StorageData> Read()
    {
        return await _fileHandler.Read(_path);
    }
    
    public async Task<Trip> GetTrip(Guid tripId)
    {
        var storageData = await Read();

        return storageData.Trips
            .FirstOrDefault(trip => trip.TripId == tripId);
    }

    public async Task<TripState> GetTripState(Guid tripId)
    {
        var storageData = await Read();

        return storageData.StateMachines
            .FirstOrDefault(sm => sm.TripId == tripId);
    }

    public async Task<decimal> GetRemainingTripBalance(Guid tripId)
    {
        var storageData = await Read();

        var remainingBalance = storageData.Trips
            .First(t => t.TripId == tripId);
        
        
       return remainingBalance.Payment.RemainingBalance;
    }

    public async Task<IEnumerable<Person>> GetTravellersForTrip(Guid tripId)
    {
        var storageData = await Read();

        var travellers = storageData.Trips
            .Select(t => new
            {
                t.TripId,
                t.Travellers
            })
            .FirstOrDefault(t => t.TripId == tripId);

        if (travellers is null || travellers.Travellers is null) return null;
        
        return travellers.Travellers.ToList();
    }

    public async Task<IEnumerable<Package>> GetPackagesForTrip(Guid tripId)
    {
        var storageData = await Read();

        var trip = storageData.Trips
            .Select(t => new
            {
                t.TripId,
                t.Packages
            })
            .FirstOrDefault(t => t.TripId == tripId);

        if (trip is null || trip.Packages is null) return null;

        return trip.Packages.ToList();
    }
}
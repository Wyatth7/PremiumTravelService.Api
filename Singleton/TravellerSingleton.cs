using System.Collections.ObjectModel;
using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Models;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Singleton;

public class TravellerSingleton : ISingleton<TravellerSingleton, Person>
{
    private static readonly Lazy<TravellerSingleton> TravellerSingletonInstance =
        new(() => new TravellerSingleton());

    public static TravellerSingleton GetInstance()
    {
        return TravellerSingletonInstance.Value;
    }

    public async Task<IReadOnlyCollection<Person>> GetData()
    {
        var fileHandler = new JsonFileHandler();

        var fileData = await fileHandler.Read(PathModel.Path);

        return new ReadOnlyCollection<Person>(fileData.Travellers);
    }
}
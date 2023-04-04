using System.Collections.ObjectModel;
using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Models;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Singleton;

public class PackageSingleton : ISingleton<PackageSingleton, Package>
{
    private static readonly Lazy<PackageSingleton> PackageSingletonInstance =
        new(() => new PackageSingleton());

    public static PackageSingleton GetInstance()
    {
        return PackageSingletonInstance.Value;
    }

    public async Task<IReadOnlyCollection<Package>> GetData()
    {
        var fileHandler = new JsonFileHandler();

        var fileData = await fileHandler.Read(PathModel.Path);

        return new ReadOnlyCollection<Package>(fileData.Packages);
    }
}
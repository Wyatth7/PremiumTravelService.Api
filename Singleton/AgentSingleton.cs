using System.Collections.ObjectModel;
using PremiumTravelService.Api.DataStorage.FileHandling;
using PremiumTravelService.Api.Models;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Services.DataStorage;

namespace PremiumTravelService.Api.Singleton;

public sealed class AgentSingleton : ISingleton<AgentSingleton, Person>
{
    private static readonly Lazy<AgentSingleton> AgentSingletonInstance =
        new(() => new AgentSingleton());

    public static AgentSingleton GetInstance()
    {
        return AgentSingletonInstance.Value;
    }

    public async Task<IReadOnlyCollection<Person>> GetData()
    {
        var fileHandler = new JsonFileHandler();

        var fileData = await fileHandler.Read(PathModel.Path);

        return new ReadOnlyCollection<Person>(fileData.Agents);
    }

    private AgentSingleton()
    {
    }
}
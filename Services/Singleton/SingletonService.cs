using PremiumTravelService.Api.Singleton;

namespace PremiumTravelService.Api.Services.Singleton;

public class SingletonService : ISingletonService
{
    public AgentSingleton GetAgentSingleton()
    {
        return AgentSingleton.GetInstance();
    }

    public TravellerSingleton GetTravellerSingleton()
    {
        return TravellerSingleton.GetInstance();
    }

    public PackageSingleton GetPackageSingleton()
    {
        return PackageSingleton.GetInstance();
    }
}
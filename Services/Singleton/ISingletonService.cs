using PremiumTravelService.Api.Singleton;

namespace PremiumTravelService.Api.Services.Singleton;

public interface ISingletonService
{
    AgentSingleton GetAgentSingleton();
    TravellerSingleton GetTravellerSingleton();
    
    PackageSingleton GetPackageSingleton();
}
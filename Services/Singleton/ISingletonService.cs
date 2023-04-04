using PremiumTravelService.Api.Singleton;

namespace PremiumTravelService.Api.Services.Singleton;

/// <summary>
/// Gets instances of singletons
/// </summary>
public interface ISingletonService
{
    /// <summary>
    /// Gets agent singleton
    /// </summary>
    /// <returns>Instance of agent singleton</returns>
    AgentSingleton GetAgentSingleton();
    
    /// <summary>
    /// Gets traveller singleton
    /// </summary>
    /// <returns>Instance of traveller singleton</returns>
    TravellerSingleton GetTravellerSingleton();
    
    /// <summary>
    /// Gets package singleton
    /// </summary>
    /// <returns>Instance of package singleton</returns>
    PackageSingleton GetPackageSingleton();
}
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.StateMachine.States;

public class PackagesState : IState
{
    public Trip Process(Trip trip, object payload)
    {
        trip.Packages ??= Array.Empty<Package>();

        var packages = trip.Packages.ToList();
        
        packages.Add((Package)payload);

        trip.Packages = packages.ToArray();

        return trip;
    }
}
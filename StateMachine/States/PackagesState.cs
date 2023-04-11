using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills;

namespace PremiumTravelService.Api.StateMachine.States;

public class PackagesState : IState
{
    public Trip Process(Trip trip, object payload)
    {
        trip.Packages ??= Array.Empty<Package>();

        var packages = trip.Packages.ToList();
        
        packages.Add((Package)payload);

        trip.Packages = packages.ToArray();

        trip.Payment = new Payment
        {
            PaymentId = Guid.NewGuid(),
            Total = trip.Packages.Sum(p => p.Total)
        };

        return trip;
    }
}
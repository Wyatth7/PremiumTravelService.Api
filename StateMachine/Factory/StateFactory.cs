using PremiumTravelService.Api.StateMachine.States;

namespace PremiumTravelService.Api.StateMachine.Factory;

public sealed class StateFactory
{
    public static IState CreateStateInstance(StateType stateType)
    {
        return stateType switch
        {
            StateType.Create => new CreateState(),
            StateType.Travellers => new TravellerState(),
            StateType.Packages => new PackagesState(),
            _ => new CreateState()
        };
    }
}
using PremiumTravelService.Api.StateMachine.States;

namespace PremiumTravelService.Api.StateMachine.Factory;

public sealed class StateFactory
{
    /// <summary>
    /// Creates an instance of a state object
    /// </summary>
    /// <param name="stateType">Type of state object to create</param>
    /// <returns>State object</returns>
    public static IState CreateStateInstance(StateType stateType)
    {
        return stateType switch
        {
            StateType.Create => new CreateState(),
            StateType.Travellers => new TravellerState(),
            StateType.Packages => new PackagesState(),
            StateType.PaymentPerson => new PaymentPersonState(),
            StateType.Payment => new PaymentState(),
            _ => new CreateState()
        };
    }
}
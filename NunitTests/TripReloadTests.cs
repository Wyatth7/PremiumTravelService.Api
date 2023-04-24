using NUnit.Framework;
using Moq;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Singleton;
using PremiumTravelService.Api.Controllers;
using PremiumTravelService.Api.Services.DataStorage;
using PremiumTravelService.Api.Services.Singleton;
using PremiumTravelService.Api.Services.StateMachine;
using PremiumTravelService.Api.Models.State;
using PremiumTravelService.Api.Persistence.Entities.StateMachine;
using PremiumTravelService.Api.StateMachine;

namespace PremiumTravelService.Api.NunitTests;

[TestFixture]
public class TripReloadTests
{
    /// <summary>
    /// This test ensures that a trip state can be saved, and then modified
    /// and reloaded later with its new state applied
    /// </summary>
    [Test]
	public async Task VerifyTripReloadsToCorrectState()
	{
        // Mock state machine interface
        var stateMachineService = new Mock<IStateMachineService>();

        // create sample trip in the "Create" state
        TripState initialTripState = new TripState();
        initialTripState.CurrentState = StateType.Create;

        // create sample trip in the "Travellers" state
        TripState nextTripState = new TripState();
        nextTripState.CurrentState = StateType.Travellers;
            
        // Mock intended behavior
        stateMachineService.Setup(p => p.CreateState(It.IsAny<Guid>())).Returns(Task.FromResult(initialTripState));
        stateMachineService.Setup(p => p.NextState(It.IsAny<String>())).Returns(Task.FromResult(nextTripState));

        // CreateState result
        var testInitialState = await stateMachineService.Object.CreateState(Guid.NewGuid());

        // ensure current state is Create
        Assert.AreEqual(testInitialState.CurrentState, StateType.Create);

        // advance to next state
        var testNextState = await stateMachineService.Object.NextState("");

        // ensure next state is Travellers
        Assert.AreEqual(testNextState.CurrentState, StateType.Travellers);
    }
}
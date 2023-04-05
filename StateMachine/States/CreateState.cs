using PremiumTravelService.Api.Services.StateMachine;

namespace PremiumTravelService.Api.StateMachine.States;

public class CreateState : StateBase
{
    public CreateState(IStateMachineService stateMachineService) : base(stateMachineService) 
    {
        State = StateType.Create;
        Status = StatusType.InProgress;
    }

    public override Task PauseState()
    {
        throw new NotImplementedException();
    }

    public override Task NextState()
    {
        throw new NotImplementedException();
    }

    public override Task UpdateState()
    {
        throw new NotImplementedException();
    }
}
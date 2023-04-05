using PremiumTravelService.Api.Services.StateMachine;

namespace PremiumTravelService.Api.StateMachine;

public abstract class StateBase
{
    protected readonly IStateMachineService StateMachineService;

    protected StateBase(IStateMachineService stateMachineService)
    {
        StateMachineService = stateMachineService;
    }
    
    protected StateType State { get; set; }
    protected StatusType Status { get; set; }

    public abstract Task PauseState();
    public abstract Task NextState();
    public abstract Task UpdateState();
}
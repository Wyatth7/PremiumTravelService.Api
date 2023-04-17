namespace PremiumTravelService.Api.Models.State;

public class MultiSelectResumeModel : StateCreationModel
{
    public IEnumerable<Guid> Payload { get; set; }
}
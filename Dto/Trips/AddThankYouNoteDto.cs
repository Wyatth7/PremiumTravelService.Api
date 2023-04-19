namespace PremiumTravelService.Api.Dto.Trips;

public class AddThankYouNoteDto
{
    public Guid TripId { get; set; }
    public string ThankYouNote { get; set; }
}
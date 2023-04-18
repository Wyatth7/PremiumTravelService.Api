namespace PremiumTravelService.Api.Dto;

public class AddPaymentPersonDto
{
    public Guid TripId { get; set; }

    public Guid AssignToPersonId { get; set; }
}
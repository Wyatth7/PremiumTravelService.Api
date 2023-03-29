using PremiumTravelService.Api.Persistence.Entities.Bills.CardInformation;

namespace PremiumTravelService.Api.Persistence.Entities.Bills.CardInformation;

public class Card
{
    public Guid CardId { get; set; }
    public string CardNumber { get; set; }
    public DateTimeOffset ExpirationDate { get; set; }
    public string SecurityCode { get; set; }
    public string NameOnCard { get; set; }
    public Address Address { get; set; }
}
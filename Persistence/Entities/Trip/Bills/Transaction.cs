using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;

namespace PremiumTravelService.Api.Persistence.Entities.Trip.Bills;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public Guid PersonId { get; set; }    
    public Card CardMovie { get; set; }
    public decimal Amount { get; set; }
}
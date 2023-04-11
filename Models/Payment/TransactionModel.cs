using PremiumTravelService.Api.Persistence.Entities.Trip.Bills;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.ChargeInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CheckInformation;
using PremiumTravelService.Api.Persistence.Enums;

namespace PremiumTravelService.Api.Models.Payment;

public class TransactionModel
{
    public Guid PersonId { get; set; }
    public PaymentType PaymentType { get; set; }
    public Card Card { get; set; }
    public Check Check { get; set; }
    public Cash Cash { get; set; }
    public decimal Total { get; set; }
}
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.ChargeInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CheckInformation;
using PremiumTravelService.Api.Persistence.Enums;

namespace PremiumTravelService.Api.Persistence.Entities.Itinerary;

public class ItineraryBilling
{
    public decimal Total { get; set; }
    public string PaidByName { get; set; }
    public PaymentType PaymentType { get; set; }
    public Card Card { get; set; }
    public Cash Cash { get; set; }
    public Check Check { get; set; }
    public ItineraryBillingDetails BillingDetails { get; set; }
}
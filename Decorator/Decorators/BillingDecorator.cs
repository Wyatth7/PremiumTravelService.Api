using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.ChargeInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CheckInformation;
using PremiumTravelService.Api.Persistence.Enums;

namespace PremiumTravelService.Api.Decorator.Decorators;

public class BillingDecorator : ItineraryDecorator
{
    public BillingDecorator(ItineraryBase itineraryBase) : base(itineraryBase)
    {
    }
    
    public override async Task<Itinerary> PopulateItinerary(Trip trip, Itinerary itinerary)
    {
        itinerary.Billing = new ItineraryBilling
        {
            Total = trip.Payment.Total,
            Transactions = GetTransactions(trip).ToArray(),
            BillingDetails = GetBillingDetails(trip).ToArray()
        };
        
        return await base.PopulateItinerary(trip, itinerary);
    }

    private List<Transactions> GetTransactions(Trip trip)
    {
        var transactions = new List<Transactions>();
        foreach (var transaction in trip.Payment.Transactions)
        {
            var finalTransaction = new Transactions
            {
                PaidByName = trip.Travellers
                    .First(t => transaction.PersonId == t.PersonId).NameFull,
                PaymentType = transaction.PaymentType,
                Amount = transaction.Amount
            };
                
            switch (transaction.PaymentType)
            {
                case PaymentType.Card:
                    finalTransaction.Card = transaction.Card;
                    break;
                case PaymentType.Check:
                    finalTransaction.Check = transaction.Check;
                    break;
                case PaymentType.Cash:
                    finalTransaction.Cash = transaction.Cash;
                    break;
            }
            
            transactions.Add(finalTransaction);
        }

        return transactions;
    }

    private List<ItineraryBillingDetails> GetBillingDetails(Trip trip)
    {
        var tripDetails = new List<ItineraryBillingDetails>();

        foreach (var package in trip.Packages)
        {
            tripDetails.Add(new()
            {
                Amount = package.Total,
                TripDetail = package.Description
            });
        }

        return tripDetails;
    }
}
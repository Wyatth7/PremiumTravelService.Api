using PremiumTravelService.Api.Persistence.Entities.Itinerary;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.ChargeInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CheckInformation;
using PremiumTravelService.Api.Persistence.Enums;
using PremiumTravelService.Api.Services.DataStorage;

namespace PremiumTravelService.Api.Decorator.Decorators;

public class BillingDecorator : ItineraryDecorator
{
    private readonly IDataStorageService _dataStorageService;
    
    public BillingDecorator(ItineraryBase itineraryBase, IDataStorageService dataStorageService) : base(itineraryBase)
    {
        _dataStorageService = dataStorageService;
    }
    
    public override async Task<Itinerary> PopulateItinerary(Trip trip, Itinerary itinerary)
    {
        var transactions = await GetTransactions(trip);
        
        itinerary.Billing = new ItineraryBilling
        {
            Total = trip.Payment.Total,
            Transactions = transactions.ToArray(),
            BillingDetails = GetBillingDetails(trip).ToArray()
        };
        
        return await base.PopulateItinerary(trip, itinerary);
    }

    private async Task<List<Transactions>> GetTransactions(Trip trip)
    {
        var transactions = new List<Transactions>();
        foreach (var transaction in trip.Payment.Transactions)
        {

            // Get list of all application travelers.
            // Not sure why all travelers in the singleton can be assigned to pay,
            // but it's in Iteration 3 requirements as such.
            var storageData = await _dataStorageService.Read();
            var paidByPerson = storageData.Travellers
                .FirstOrDefault(p => p.PersonId == trip.Payment.AssignedToPerson.PersonId);
            
            var finalTransaction = new Transactions
            {
                PaidByName = paidByPerson is not null ? paidByPerson.NameFull : "Traveler",
                PaymentType = transaction.PaymentType,
                Amount = transaction.Amount
            };
                
            // assign payment type
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

    /// <summary>
    /// Gets billing details of a trip
    /// </summary>
    /// <param name="trip">trip to get details from</param>
    /// <returns>itinerary billing details object</returns>
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
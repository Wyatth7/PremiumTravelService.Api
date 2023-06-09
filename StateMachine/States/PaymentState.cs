using System.ComponentModel;
using PremiumTravelService.Api.Models.Payment;
using PremiumTravelService.Api.Persistence.Entities.Trip;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills;
using PremiumTravelService.Api.Persistence.Enums;

namespace PremiumTravelService.Api.StateMachine.States;

public class PaymentState : IState
{
    public Trip Process(Trip trip, object payload)
    {
        var transactionData = (TransactionModel) payload;
        
        var transaction = new Transaction {TransactionId = Guid.NewGuid()};

        // find if cash, card, check
        switch (transactionData.PaymentType)
        {
            case PaymentType.Card:
                transaction.Card = transactionData.Card;
                transaction.Card.CardId = Guid.NewGuid();
                transaction.Card.Address.AddressId = Guid.NewGuid();
                transactionData.Check = null;
                transactionData.Cash = null;
                transaction.Loan = null;
                break;
            case PaymentType.Check:
                transaction.Check = transactionData.Check;
                transaction.Check.CheckId = Guid.NewGuid();
                transaction.Card = null;
                transaction.Cash = null;
                transaction.Loan = null;
                break;
            case PaymentType.Cash:
                transaction.Card = null;
                transaction.Check = null;
                transaction.Loan = null;
                break;
            case PaymentType.Loan:
                transaction.Loan = transactionData.Loan;
                transaction.Card = null;
                transaction.Check = null;
                transaction.Cash = null;
                break;
            default:
                throw new InvalidEnumArgumentException("The enum value provided is not valid " +
                                                       "in the PaymentType enum.");
        }

        // apply amount to trip total
        transaction.Amount = transactionData.Total;
        transaction.PaymentType = transactionData.PaymentType;

        if (trip.Payment.Transactions is null)
        {
            trip.Payment.Transactions = new[] {transaction};
        }
        else
        {
            var transactionList = trip.Payment.Transactions.ToList();
            
            transactionList.Add(transaction);

            trip.Payment.Transactions = transactionList.ToArray();
        }

        if (trip.Payment.Transactions.Sum(t => t.Amount) >= trip.Payment.Total)
        {
            trip.Payment.IsPaidInFull = true;
        }

        return trip;
    }
}
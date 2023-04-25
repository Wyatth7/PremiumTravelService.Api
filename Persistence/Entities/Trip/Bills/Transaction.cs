﻿using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.ChargeInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CheckInformation;
using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.LoanInformation;
using PremiumTravelService.Api.Persistence.Enums;

namespace PremiumTravelService.Api.Persistence.Entities.Trip.Bills;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public Guid PersonId { get; set; }
    public PaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
    public Card Card { get; set; }
    public Check Check { get; set; }
    public Cash Cash { get; set; }
    public Loan Loan { get; set; }
}
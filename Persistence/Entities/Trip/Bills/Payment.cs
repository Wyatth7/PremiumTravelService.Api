using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PremiumTravelService.Api.Persistence.Entities.Trip.Bills;

public class Payment
{
    public Guid PaymentId { get; set; }
    public Person AssignedToPerson { get; set; }
    public decimal Total { get; set; }
    
    [JsonIgnore]
    public decimal RemainingBalance => Transactions is null ? Total : Total - Transactions.Sum(t => t.Amount);
    public bool IsPaidInFull { get; set; }
    public BillDetail[] BillingDetails { get; set; }
    public Transaction[] Transactions { get; set; }
}
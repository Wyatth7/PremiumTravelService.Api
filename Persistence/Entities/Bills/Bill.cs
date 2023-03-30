namespace PremiumTravelService.Api.Persistence.Entities.Bills;

public class Bill
{
    public Guid BillId { get; set; }
    public Person AssignedByPerson { get; set; }
    public decimal Total { get; set; }
    public decimal RemainingBalance { get; set; }
    public bool IsPaidInFull { get; set; }
    public BillDetail[] BillingDetails { get; set; }
    public Transaction[] Transactions { get; set; }
}
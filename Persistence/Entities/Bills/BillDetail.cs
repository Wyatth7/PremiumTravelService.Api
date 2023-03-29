namespace PremiumTravelService.Api.Persistence.Entities.Bills;

public class BillDetail
{
    public Guid BillDetailId { get; set; }
    public string Description { get; set; }
    public decimal Total { get; set; }
}
namespace PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CheckInformation;

public class Check
{
    public Guid CheckId { get; set; }
    public string RoutingNumber { get; set; }
    public string AccountNumber { get; set; }
    public string CheckNumber { get; set; }
}
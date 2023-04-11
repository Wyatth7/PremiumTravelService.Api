using PremiumTravelService.Api.Persistence.Entities.Trip.Bills.CardInformation;

namespace PremiumTravelService.Api.Persistence.Entities.Trip;

public class Person
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsEmployee { get; set; }
    public Card CardDetails { get; set; }
    
    public string NameFull => $"{FirstName} {LastName}";
}
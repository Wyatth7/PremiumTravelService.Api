namespace PremiumTravelService.Api.Persistence.Entities.Trip.Bills.LoanInformation;

public class Loan
{
    public decimal LoanAmount { get; set; }
    public decimal TotalPayment { get; set; }
    public decimal MonthlyPayment { get; set; }
    public int LoanPeriod { get; set; }
    public decimal LoanRate { get; set; }
}
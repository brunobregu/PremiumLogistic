using Microsoft.Identity.Client.Extensions.Msal;

namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class DetailsDto
{
    public int NumberOfOrders { get; set; }
    public int ClientTotal { get; set; }
    public int ToBePaid { get; set; }
    public int Paid => ClientTotal - ToBePaid;
}

namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class DetailsDto
{
    public int NumberOfOrders { get; set; }
    public int SumClientTotal { get; set; }
    public int SumPartlyPaid { get; set; }
    public int SumToBePaid { get; set; }
}

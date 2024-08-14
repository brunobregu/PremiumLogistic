namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class UpdateCarStatusDto
{
    public List<IFormFile>? Photos { get; set; }

    public string? TrackingNumber { get; set; }
    public List<IFormFile>? Documents { get; set; }
}

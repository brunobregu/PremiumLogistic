namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class UpdateCarStatusDto
{
    [Required(ErrorMessage = "Status is required!")]
    public string Status { get; set; }
    public List<IFormFile>? Photos { get; set; }

    public string? TrackingNumber { get; set; }
    public List<IFormFile>? Documents { get; set; }
}

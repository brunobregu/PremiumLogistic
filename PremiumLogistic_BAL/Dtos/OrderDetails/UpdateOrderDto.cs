namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class UpdateOrderDto
{
    [Required(ErrorMessage = "VIN is required")]
    public string VIN { get; set; }
    [Required(ErrorMessage = "Make is required")]
    public string Make { get; set; }
    [Required(ErrorMessage = "Model is required")]
    public string Model { get; set; }
    [Required(ErrorMessage = "Year is required")]
    [Range(1900, int.MaxValue, ErrorMessage = "Year must be at least 1900")]
    public int Year { get; set; }
    [Required(ErrorMessage = "Lot is required")]
    public int Lot { get; set; }
    [Required(ErrorMessage = "Order id is required")]
    public string OrderID { get; set; }
    public string? TrackingNumber { get; set; }
    [Required(ErrorMessage = "Auction is required")]
    public string Auction { get; set; }
    [Required(ErrorMessage = "Port is required")]
    public string Port { get; set; }
    public string Provider { get; set; }
    [Required(ErrorMessage = "Inland price is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Inland price must be at least 1")]
    public int InlandPrice { get; set; }
    [Required(ErrorMessage = "Ocean price is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Ocean price must be at least 1")]
    public int OceanPrice { get; set; }
    public int Broker { get; set; }
    public int ClientStorage { get; set; } = 0;
    public int ClientTotal => InlandPrice + OceanPrice + Broker + ClientStorage;
    [Required(ErrorMessage = "Inland cost is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Inland cost must be at least 1")]
    public int InlandCost { get; set; }
    [Required(ErrorMessage = "Ocean cost is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Ocean cost must be at least 1")]
    public int OceanCost { get; set; }
    public int StorageCost { get; set; } = 0;
    public int TotalCost => InlandCost + OceanCost + StorageCost;
    public int Profit => ClientTotal - TotalCost;
    [AllowedValues("Not Paid", "Partly Paid", "Paid")]
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; } = 0;
    public int ToBePaid => PaymentStatus == "Paid" ? 0 : ClientTotal - PartlyPaid;
    public string UserId { get; set; }
}

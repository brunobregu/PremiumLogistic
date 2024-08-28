namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class AddOrderDetailsDto
{
    [Required(ErrorMessage = "VINRequired")]
    public string VIN { get; set; }
    [Required(ErrorMessage = "MakeRequired")]
    public string Make { get; set; }
    [Required(ErrorMessage = "ModelRequired")]
    public string Model { get; set; }
    [Required(ErrorMessage = "YearRequired")]
    public int Year { get; set; }
    [Required(ErrorMessage = "LotRequired")]
    public int Lot { get; set; }
    [Required(ErrorMessage = "OrderIdRequired")]
    public string OrderID { get; set; }
    [Required(ErrorMessage = "AuctionRequired")]
    public string Auction { get; set; }
    [Required(ErrorMessage = "PortRequired")]
    public string Port { get; set; }
    public string? Provider { get; set; }
    [Required(ErrorMessage = "InlandPriceRequired")]
    [Range(1, int.MaxValue, ErrorMessage = "InlandPriceMin")]
    public int InlandPrice { get; set; }
    [Required(ErrorMessage = "OceanPriceRequired")]
    [Range(1, int.MaxValue, ErrorMessage = "OceanPriceMin")]
    public int OceanPrice { get; set; }
    public int Broker { get; set; } = 0;
    public int ClientStorage { get; set; } = 0;
    public int CarPrice { get; set; } = 0;
    public int ClientTotal => InlandPrice + OceanPrice + Broker + ClientStorage + CarPrice;
    [Required(ErrorMessage = "InlandCostRequired")]
    [Range(1, int.MaxValue, ErrorMessage = "InlandCostMin")]
    public int InlandCost { get; set; }
    [Required(ErrorMessage = "OceanCostRequired")]
    [Range(1, int.MaxValue, ErrorMessage = "OceanCostMin")]
    public int OceanCost { get; set; }
    public int StorageCost { get; set; } = 0;
    public int TotalCost => InlandCost + OceanCost + StorageCost;
    public int Profit => ClientTotal - TotalCost;
    [AllowedValues("Not Paid", "Partly Paid", "Paid", ErrorMessage = "PaymentStatusAllowedValues")]
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; } = 0;
    public int ToBePaid => PaymentStatus == "Paid" ? 0 : ClientTotal - PartlyPaid;
    [Required(ErrorMessage = "Please assign a client to the order")]
    public string UserId { get; set; }
}

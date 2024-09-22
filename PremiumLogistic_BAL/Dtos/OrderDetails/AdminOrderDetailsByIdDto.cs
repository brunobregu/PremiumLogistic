namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class AdminOrderDetailsByIdDto
{
    public int Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public string VIN { get; set; } 
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Lot { get; set; }
    public string OrderID { get; set; }
    public string Auction { get; set; }
    public string? TrackingNumber { get; set; }
    public string CarStatus { get; set; }
    public string? Provider { get; set; }
    public string Port { get; set; }
    public int InlandPrice { get; set; }
    public int OceanPrice { get; set; }
    public int Broker { get; set; }
    public int ClientStorage { get; set; }
    public int CarPrice { get; set; }
    public int ClientTotal { get; set; }
    public int InlandCost { get; set; }
    public int OceanCost { get; set; }
    public int StorageCost { get; set; }
    public int CarCost { get; set; }
    public int TotalCost { get; set; }
    public int Profit { get; set; }
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; }
    public int ToBePaid { get; set; }
    public string UserId { get; set; }
    public string FullName { get; set; }
}

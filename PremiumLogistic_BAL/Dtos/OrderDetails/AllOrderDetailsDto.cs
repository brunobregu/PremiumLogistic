namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class AllOrderDetailsDto
{
    public int Id { get; set; }
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Lot { get; set; }
    public string DspOrderID { get; set; }
    public string Auction { get; set; }
    public string CarStatus { get; set; }
    public string Port { get; set; }
    public int ClientTotal { get; set; }
    public int TotalCost { get; set; }
    public string PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public int PartlyPaid { get; set; }
    public string Fullname { get; set; }
}
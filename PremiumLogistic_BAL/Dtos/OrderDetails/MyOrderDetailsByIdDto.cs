namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class MyOrderDetailsByIdDto
{
    public int Id { get; set; }
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Lot { get; set; }
    public string Port { get; set; }
    public string Auction { get; set; }
    public string CarStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public string Link { get; set; }
    public int InlandPrice { get; set; }
    public int OceanPrice { get; set; }
    public int Broker { get; set; }
    public int ClientStorage { get; set; }
    public int CarPrice { get; set; }
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; }
}

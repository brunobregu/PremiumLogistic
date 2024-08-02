namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class OrderDetailsByIdDto
{
    public int Id { get; set; }
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int Lot { get; set; }
    public string DspOrderID { get; set; }
    public string Port { get; set; }
    public int InlandCargoloop { get; set; }
    public int OcCargoloop { get; set; }
    public int Broker { get; set; }
    public int InlandDspch { get; set; }
    public int OcCost { get; set; }
    public int ClientStorage { get; set; }
    public int StorageCost { get; set; }
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; }
    public string UserId { get; set; }
    public string FullName { get; set; }
}

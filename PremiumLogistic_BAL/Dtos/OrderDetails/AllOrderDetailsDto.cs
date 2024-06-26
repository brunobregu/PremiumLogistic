namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class AllOrderDetailsDto
{
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Lot { get; set; }
    public string DspOrderID { get; set; }
    public string Port { get; set; }
    public int InlandCargoloop { get; set; }
    public int OcCargoloop { get; set; }
    public int Broker { get; set; }
    public int InlandDspch { get; set; }
    public int OcCost { get; set; }
    public int Storage { get; set; }
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; }
    public string Fullname { get; set; }
}
namespace PremiumLogistic_BAL.Dtos.OrderDetails;

public class AddOrderDetailsDto
{
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
    public int ClientTotal => InlandCargoloop + OcCargoloop + Broker;
    public int InlandDspch { get; set; }
    public int OcCost { get; set; }
    public int TotalCost => InlandDspch + OcCost;
    public int Profit => ClientTotal - TotalCost;
    public int Storage { get; set; }
    [AllowedValues("Paid", "Not paid", "Partly Paid")]
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; }
    public int ToBePaid => ClientTotal - PartlyPaid;
    public string UserId { get; set; }
}

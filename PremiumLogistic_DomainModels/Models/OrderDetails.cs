namespace PremiumLogistic_DomainModels.Models;

public class OrderDetails
{
    public int Id { get; set; }
    public bool Invalidated { get; set; } = false;
    public DateTime? CreatedOn { get; set; }
    [StringLength(50)]
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    [StringLength(50)]
    public string? UpdatedBy { get; set; }
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
    public int ClientTotal { get; set; }
    public int InlandDspch { get; set; }
    public int OcCost { get; set; }
    public int TotalCost { get; set; }
    public int Profit { get; set; }
    public int Storage { get; set; }
    public string PaymentStatus { get; set; }
    public int PartlyPaid { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
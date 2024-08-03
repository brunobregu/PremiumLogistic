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
    [Required]
    public string VIN { get; set; } //shasia e makines
    [Required]
    public string Make { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public int Year { get; set; }
    [Required]
    public int Lot { get; set; } //nr identifikues ne coopart/iaai
    [Required]
    public string OrderID { get; set; } //nr gjurmimi per makinat qe marrin karrotrec
    [Required]
    public string Auction { get; set; }
    public string? TrackingNumber { get; set; }
    [Required]
    public string CarStatus { get; set; }
    public string? Provider { get; set; }
    [Required]
    public string Port { get; set; }
    [Required]
    public int InlandPrice { get; set; }
    [Required]
    public int OceanPrice { get; set; }
    public int Broker { get; set; } //komision qe mban kompania
    public int ClientStorage { get; set; } //vonesat ne pagese
    [Required]
    public int ClientTotal { get; set; }
    [Required]
    public int InlandCost { get; set; }
    [Required]
    public int OceanCost { get; set; }
    public int StorageCost { get; set; } //vonesat ne pagese
    [Required]
    public int TotalCost { get; set; }
    [Required]
    public int Profit { get; set; }
    public string PaymentStatus { get; set; }
    [Required]
    public int PartlyPaid { get; set; }
    [Required]
    public int ToBePaid { get; set; }
    public string? PhotosPath { get; set; }
    public string? DocumentsPath { get; set; }
    [Required]
    [StringLength(450)]
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
namespace PremiumLogistic_DomainModels.Models;

public class LocalTransportation : CommonAttributes
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string AuctionLocation { get; set; }
    [Required]
    [StringLength(20)]
    public string Auction { get; set; }
    [Required]
    [StringLength(100)]
    public string City { get; set; }
    [Required]
    [StringLength(20)]
    public string State { get; set; }
    [Required]
    [StringLength(20)]
    public string Zip { get; set; }
    [Required]
    public decimal Savannah_GA { get; set; }
    [Required]
    public decimal Elizabeth_NJ { get; set; }
    [Required]
    public decimal Houston_TX { get; set; }
    [Required]
    public decimal LosAngeles_CA { get; set; }
    [Required]
    public decimal Indianapolis_IN { get; set; }
}

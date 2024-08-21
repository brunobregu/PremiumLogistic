namespace PremiumLogistic_DomainModels.Models;

public class Transportation
{
    [Key]
    [StringLength(50)]
    public string Zip { get; set; }
    public bool Invalidated { get; set; } = false;
    [StringLength(256)]
    public string AuctionLocation { get; set; }
    [StringLength(50)]
    public string Auction { get; set; }
    [StringLength(50)]
    public string City { get; set; }
    [StringLength(50)]
    public string State { get; set; }
    public int Savannah { get; set; }
    public int Elizabeth { get; set; }
    public int Houston { get; set; }
    public int LosAngeles { get; set; }
    public int Indianapolis { get; set; }
}

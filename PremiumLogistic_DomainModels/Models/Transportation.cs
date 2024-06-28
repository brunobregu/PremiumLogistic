namespace PremiumLogistic_DomainModels.Models;

public class Transportation
{
    [Key]
    public string Zip { get; set; }
    public bool Invalidated { get; set; } = false;
    public string AuctionLocation { get; set; }
    public string Auction { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public int Savannah { get; set; }
    public int Elizabeth { get; set; }
    public int Houston { get; set; }
    public int LosAngeles { get; set; }
    public int Indianapolis { get; set; }
}

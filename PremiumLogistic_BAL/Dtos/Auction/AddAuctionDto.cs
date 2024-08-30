namespace PremiumLogistic_BAL.Dtos.Auction;

public class AddAuctionDto
{
    [Required(ErrorMessage = "AuctionRequired")]
    [StringLength(50, ErrorMessage = "AuctionChar")]
    public string Name { get; set; }
}

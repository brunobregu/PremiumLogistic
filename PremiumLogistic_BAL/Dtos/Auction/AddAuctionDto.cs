namespace PremiumLogistic_BAL.Dtos.Auction;

public class AddAuctionDto
{
    [Required(ErrorMessage = "Auction is required")]
    [StringLength(50, ErrorMessage = "Auction name should be at most 50 characteres")]
    public string Name { get; set; }
}

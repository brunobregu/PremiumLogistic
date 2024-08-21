namespace PremiumLogistic_BAL.Dtos.Auction;

public class AddAuctionDto
{
    [Required(ErrorMessage = "Auction is required")]
    public string Name { get; set; }
}

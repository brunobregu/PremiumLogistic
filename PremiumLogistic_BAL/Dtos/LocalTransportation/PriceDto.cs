namespace PremiumLogistic_BAL.Dtos.LocalTransportation;

public class PriceDto
{
    public int Land { get; set; }
    public int Ocean { get; set; }
    public int Total => Land + Ocean;
}

namespace PremiumLogistic_DomainModels.Models;

public class Ocean
{
    public int Id { get; set; }
    public bool Invalidated { get; set; } = false;
    public string Port { get; set; }
    public int Savannah { get; set; }
    public int Elizabeth { get; set; }
    public int Houston { get; set; }
    public int LosAngeles { get; set; }
    public int Indianapolis { get; set; }
}

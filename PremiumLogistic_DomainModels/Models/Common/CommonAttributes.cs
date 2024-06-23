namespace PremiumLogistic_DomainModels.Models.Common;

public class CommonAttributes
{
    public bool Invalidated { get; set; } = false;
    public DateTime? CreatedOn { get; set; }
    [StringLength(50)]
    public string? CreatedBy { get; set; }
    [StringLength(20)]
    public string? CreatedIP { get; set; }
    public DateTime? UpdatedOn { get; set; }
    [StringLength(50)]
    public string? UpdatedBy { get; set; }
    [StringLength(20)]
    public string? UpdatedIP { get; set; }
}

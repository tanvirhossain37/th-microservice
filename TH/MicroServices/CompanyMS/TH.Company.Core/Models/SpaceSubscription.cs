namespace TH.CompanyMS.Core;

public class SpaceSubscription : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public int PlanId { get; set; }
	public bool IsCurrent { get; set; }
	public string? CardHolderName { get; set; }
	public string? CardNumber { get; set; }
	public string? SecurityCode { get; set; }
	public DateTime? CardExpiryDate { get; set; }
	public string? CountryId { get; set; }
	public string? ZipCode { get; set; }
}
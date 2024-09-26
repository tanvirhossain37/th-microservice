namespace TH.CompanyMS.Core;

public class BranchUser : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string BranchId { get; set; } = null!;
	public string UserId { get; set; } = null!;
	public bool IsDefault { get; set; }
	public virtual Branch Branch { get; set; } = null!;
	public virtual Company Company { get; set; } = null!;
	public virtual User User { get; set; } = null!;
}
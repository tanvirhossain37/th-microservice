namespace TH.CompanyMS.Core;

public class Branch : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string? Code { get; set; }
	public string Name { get; set; } = null!;
	public bool IsDefault { get; set; }
	public virtual ICollection<BranchUser> BranchUsers { get; set; } = new List<BranchUser>();
	public virtual Company Company { get; set; } = null!;
}
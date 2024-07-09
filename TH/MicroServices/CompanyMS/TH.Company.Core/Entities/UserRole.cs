namespace TH.CompanyMS.Core;

public class UserRole : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string UserId { get; set; } = null!;
	public string RoleId { get; set; } = null!;
	public virtual Company Company { get; set; } = null!;
	public virtual Role Role { get; set; } = null!;
	public virtual User User { get; set; } = null!;
}
namespace TH.CompanyMS.Core;

public class Role : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string? Code { get; set; }
	public string Name { get; set; } = null!;
	public virtual Company Company { get; set; } = null!;
	public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
	public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
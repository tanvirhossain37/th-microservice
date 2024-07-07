namespace TH.Company.Core;

public class Company : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string? Code { get; set; }
	public string? Website { get; set; }
	public string? Slogan { get; set; }
	public string? Logo { get; set; }
	public virtual ICollection<BranchUser> BranchUsers { get; set; } = new List<BranchUser>();
	public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
	public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
	public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
	public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
	public virtual ICollection<User> Users { get; set; } = new List<User>();
}
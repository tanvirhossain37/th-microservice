namespace TH.Company.Core;

public class User : BaseEntity
{

    public string SpaceId { get; set; } = null!;
    public string CompanyId { get; set; } = null!;
    public int UserTypeId { get; set; }
    public virtual ICollection<BranchUser> BranchUsers { get; set; } = new List<BranchUser>();
    public virtual Company Company { get; set; } = null!;
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
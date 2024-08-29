using TH.Common.Model;

namespace TH.CompanyMS.Core;

public class UserCompany : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public int TypeId { get; set; }
	public int StatusId { get; set; }
	public string UserId { get; set; } = null!;
	public virtual Company Company { get; set; } = null!;
	public virtual User User { get; set; } = null!;
}
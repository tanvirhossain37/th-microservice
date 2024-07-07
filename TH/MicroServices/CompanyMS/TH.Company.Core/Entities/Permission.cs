namespace TH.Company.Core;

public class Permission : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string RoleId { get; set; } = null!;
	public string ModuleId { get; set; } = null!;
	public bool Read { get; set; }
	public bool Write { get; set; }
	public bool Update { get; set; }
	public bool Delete { get; set; }
	public int AccessTypeId { get; set; }
	public virtual Company Company { get; set; } = null!;
	public virtual Module Module { get; set; } = null!;
	public virtual Role Role { get; set; } = null!;
}
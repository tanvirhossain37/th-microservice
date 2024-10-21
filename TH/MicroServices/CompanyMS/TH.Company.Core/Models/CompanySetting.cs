namespace TH.CompanyMS.Core;

public class CompanySetting : BaseEntity
{
    
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string Key { get; set; } = null!;
	public bool Value { get; set; }
	public int ModuleId { get; set; }
	public virtual Company Company { get; set; } = null!;
}
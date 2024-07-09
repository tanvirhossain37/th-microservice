namespace TH.CompanyMS.Core;

public class Module : BaseEntity
{
    
	public string Name { get; set; } = null!;
	public string? Code { get; set; }
	public string? Route { get; set; }
	public string? Icon { get; set; }
	public int Level { get; set; }
	public string? ParentId { get; set; }
	public int Order { get; set; }
	public virtual ICollection<Module> InverseParent { get; set; } = new List<Module>();
	public virtual Module? Parent { get; set; }
	public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
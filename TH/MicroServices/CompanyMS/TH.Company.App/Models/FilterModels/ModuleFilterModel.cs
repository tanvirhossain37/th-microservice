using System.Collections.Generic;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial class ModuleFilterModel
{   
	public string Id { get; set; } = null!;
	public DateTime? CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool? Active { get; set; }
	public string Name { get; set; } = null!;
	public string? ControllerName { get; set; }
	public string? Route { get; set; }
	public string? Icon { get; set; }
	public string? ParentId { get; set; }
	public int MenuOrder { get; set; }
}
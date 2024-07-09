using System.Collections.Generic;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial class RoleFilterModel
{   
	public string Id { get; set; } = null!;
	public DateTime? CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool? Active { get; set; }
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string? Code { get; set; }
	public string Name { get; set; } = null!;
}
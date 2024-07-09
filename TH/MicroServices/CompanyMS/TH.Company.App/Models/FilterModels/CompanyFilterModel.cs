using System.Collections.Generic;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial class CompanyFilterModel
{   
	public string Id { get; set; } = null!;
	public DateTime? CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool? Active { get; set; }
	public string SpaceId { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string? Code { get; set; }
	public string? Website { get; set; }
	public string? Slogan { get; set; }
	public string? Logo { get; set; }
}
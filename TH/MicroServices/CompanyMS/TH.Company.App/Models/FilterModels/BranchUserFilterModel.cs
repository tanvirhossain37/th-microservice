using System.Collections.Generic;
using TH.Common.Model;
using TH.MongoRnDMS.App;

namespace TH.Company.App;

public partial class BranchUserFilterModel
{   
	public string Id { get; set; } = null!;
	public DateTime? CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool? Active { get; set; }
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string BranchId { get; set; } = null!;
	public string UserId { get; set; } = null!;
	public bool? IsDefault { get; set; }
}
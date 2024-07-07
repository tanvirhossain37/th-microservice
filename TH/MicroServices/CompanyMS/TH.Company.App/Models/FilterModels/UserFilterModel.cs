using System.Collections.Generic;
using TH.Common.Model;
using TH.MongoRnDMS.App;

namespace TH.Company.App;

public partial class UserFilterModel
{   
	public string Id { get; set; } = null!;
	public DateTime? CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool? Active { get; set; }
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public int UserTypeId { get; set; }
}
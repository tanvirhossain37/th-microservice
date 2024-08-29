using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class UserCompanyViewModel
{   
	public string Id { get; set; } = null!;
	public DateTime CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool Active { get; set; }
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public int TypeId { get; set; }
	public int StatusId { get; set; }
	public string UserId { get; set; } = null!;
}
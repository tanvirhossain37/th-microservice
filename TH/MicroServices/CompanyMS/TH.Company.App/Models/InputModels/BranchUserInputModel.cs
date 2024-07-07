using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Company.App;

public partial class BranchUserInputModel
{   
	public string Id { get; set; } = null!;
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string BranchId { get; set; } = null!;
	public string UserId { get; set; } = null!;
	public bool IsDefault { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class UserInputModel
{   
	public string Id { get; set; } = null!;
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public int UserTypeId { get; set; }
	public string Name { get; set; } = null!;
	public string UserName { get; set; } = null!;
}
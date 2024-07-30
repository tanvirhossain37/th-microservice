using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class PermissionInputModel
{   
	public string Id { get; set; } = null!;
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string RoleId { get; set; } = null!;
	public string ModuleId { get; set; } = null!;
	public bool Read { get; set; }
	public bool Write { get; set; }
	public bool Update { get; set; }
	public bool Delete { get; set; }
	public string? ParentId { get; set; }
	public int MenuOrder { get; set; }
}
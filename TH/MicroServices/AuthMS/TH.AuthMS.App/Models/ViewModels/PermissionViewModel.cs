using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.AuthMS.App;

public partial class PermissionViewModel
{   
	public string Id { get; set; } = null!;
	public DateTime CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool Active { get; set; }
	public string SpaceId { get; set; } = null!;
	public string CompanyId { get; set; } = null!;
	public string RoleId { get; set; } = null!;
	public string ModuleId { get; set; } = null!;
	public bool Read { get; set; }
	public bool Write { get; set; }
	public bool Update { get; set; }
	public bool Delete { get; set; }
	public int AccessTypeId { get; set; }
}
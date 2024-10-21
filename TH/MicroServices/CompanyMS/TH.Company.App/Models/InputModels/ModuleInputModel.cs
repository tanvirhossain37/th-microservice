using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class ModuleInputModel
{   
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string? ControllerName { get; set; }
	public string? Route { get; set; }
	public string? Icon { get; set; }
	public string? ParentId { get; set; }
	public int MenuOrder { get; set; }
	public int Level { get; set; }
}
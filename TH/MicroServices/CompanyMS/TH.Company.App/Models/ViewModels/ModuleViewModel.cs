using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Company.App;

public partial class ModuleViewModel
{   
	public string Id { get; set; } = null!;
	public DateTime CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool Active { get; set; }
	public string Name { get; set; } = null!;
	public string? Code { get; set; }
	public string? Route { get; set; }
	public string? Icon { get; set; }
	public int Level { get; set; }
	public string? ParentId { get; set; }
	public int Order { get; set; }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Company.App;

public partial class CompanyInputModel
{   
	public string Id { get; set; } = null!;
	public string SpaceId { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string? Code { get; set; }
	public string? Website { get; set; }
	public string? Slogan { get; set; }
	public string? Logo { get; set; }
}
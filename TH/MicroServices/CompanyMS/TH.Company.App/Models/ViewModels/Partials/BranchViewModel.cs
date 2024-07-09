using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class BranchViewModel
{   
	public string SpaceName { get; set; }
	public string CompanyName { get; set; }
}
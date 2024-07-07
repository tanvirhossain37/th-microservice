using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Company.App;

public partial class RoleViewModel
{   
	public string SpaceName { get; set; }
	public string CompanyName { get; set; }
}
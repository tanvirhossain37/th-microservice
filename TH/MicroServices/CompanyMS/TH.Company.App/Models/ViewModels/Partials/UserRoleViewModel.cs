using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class UserRoleViewModel
{   
	public string SpaceName { get; set; }
	public string CompanyName { get; set; }
	public string UserName { get; set; }
	public string RoleName { get; set; }
}
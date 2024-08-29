using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class UserCompanyViewModel
{   
	public string SpaceName { get; set; }
	public string CompanyName { get; set; }
	public string TypeName { get; set; }
	public string StatusName { get; set; }
	public string UserName { get; set; }
    public CompanyViewModel Company { get; set; }
    public UserViewModel User { get; set; }
}
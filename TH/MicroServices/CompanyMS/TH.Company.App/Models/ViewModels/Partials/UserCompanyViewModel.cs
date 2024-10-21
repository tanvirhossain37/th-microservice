using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class UserCompanyViewModel
{   
	public string SpaceName { get; set; }
	public string CompanyName { get; set; }
    public string CompanyCode { get; set; }
    public string CompanyWebsite { get; set; }
    public string CompanySlogan { get; set; }
    public string CompanyLogo { get; set; }
    public string TypeName { get; set; }
	public string StatusName { get; set; }
	public string UserName { get; set; }
    public string DefaultBranchId { get; set; }
    public string DefaultBranchName { get; set; }
    public string DefaultBranchCode { get; set; }
}
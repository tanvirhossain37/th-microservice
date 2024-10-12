using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class SpaceSubscriptionViewModel
{   
	public string SpaceName { get; set; }
	public string PlanName { get; set; }
	public string CountryName { get; set; }
}
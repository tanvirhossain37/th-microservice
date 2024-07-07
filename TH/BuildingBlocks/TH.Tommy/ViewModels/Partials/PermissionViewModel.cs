using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Company.App;

public partial class PermissionViewModel
{   
		public string SpaceName { get; set; }
		public string CompanyName { get; set; }
		public string RoleName { get; set; }
		public string ModuleName { get; set; }
		public string AccessTypeName { get; set; }
}
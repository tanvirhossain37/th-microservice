using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Company.App;

public partial class UserViewModel
{   
		public string SpaceName { get; set; }
		public string CompanyName { get; set; }
		public string UserTypeName { get; set; }
}
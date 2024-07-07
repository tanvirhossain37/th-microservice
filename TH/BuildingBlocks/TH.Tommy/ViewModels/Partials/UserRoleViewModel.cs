using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.Company.App;

public partial class UserRoleViewModel
{   
		public string SpaceName { get; set; }
		public string CompanyName { get; set; }
		public string UserFirstName { get; set; }
		public string UserSurname { get; set; }
		public string UserEmail { get; set; }
		public string RoleName { get; set; }
}
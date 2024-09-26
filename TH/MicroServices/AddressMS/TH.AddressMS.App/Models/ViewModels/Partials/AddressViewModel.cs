using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.AddressMS.App;

public partial class AddressViewModel
{   
	public string CountryName { get; set; }
	public string ClientName { get; set; }
}
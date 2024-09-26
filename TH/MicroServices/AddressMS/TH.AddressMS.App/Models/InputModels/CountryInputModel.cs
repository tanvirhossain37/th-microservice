using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.AddressMS.App;

public partial class CountryInputModel
{   
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string Code { get; set; } = null!;
	public string IsoCode { get; set; } = null!;
	public string CurrencyName { get; set; } = null!;
	public string CurrencyCode { get; set; } = null!;
}
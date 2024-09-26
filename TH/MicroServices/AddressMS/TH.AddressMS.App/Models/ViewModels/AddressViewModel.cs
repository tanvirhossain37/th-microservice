using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.AddressMS.App;

public partial class AddressViewModel
{   
	public string Id { get; set; } = null!;
	public DateTime CreatedDate { get; set; }
	public DateTime? ModifiedDate { get; set; }
	public bool Active { get; set; }
	public string Street { get; set; } = null!;
	public string City { get; set; } = null!;
	public string? State { get; set; }
	public string PostalCode { get; set; } = null!;
	public string CountryId { get; set; } = null!;
	public string ClientId { get; set; } = null!;
}
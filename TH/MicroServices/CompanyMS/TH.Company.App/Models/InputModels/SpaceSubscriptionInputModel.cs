using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TH.CompanyMS.App;

public partial class SpaceSubscriptionInputModel
{   
	public string Id { get; set; } = null!;
	public string SpaceId { get; set; } = null!;
	public int PlanId { get; set; }
	public bool IsCurrent { get; set; }
	public string? CardHolderName { get; set; }
	public string? CardNumber { get; set; }
	public string? SecurityCode { get; set; }
	public DateTime? CardExpiryDate { get; set; }
	public string? CountryId { get; set; }
	public string? ZipCode { get; set; }
}
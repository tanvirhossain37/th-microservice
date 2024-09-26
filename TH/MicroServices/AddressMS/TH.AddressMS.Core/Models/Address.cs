namespace TH.AddressMS.Core;

public class Address : BaseEntity
{
    
	public string Street { get; set; } = null!;
	public string City { get; set; } = null!;
	public string? State { get; set; }
	public string PostalCode { get; set; } = null!;
	public string CountryId { get; set; } = null!;
	public string ClientId { get; set; } = null!;
	public virtual Country Country { get; set; } = null!;
}
namespace TH.AddressMS.Core;

public class Country : BaseEntity
{
    
	public string Name { get; set; } = null!;
	public string Code { get; set; } = null!;
	public string IsoCode { get; set; } = null!;
	public string CurrencyName { get; set; } = null!;
	public string CurrencyCode { get; set; } = null!;
	public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
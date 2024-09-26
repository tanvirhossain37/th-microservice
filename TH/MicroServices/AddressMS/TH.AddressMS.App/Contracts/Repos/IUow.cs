namespace TH.AddressMS.App;

public interface IUow : IDisposable
{   
	IAddressRepo AddressRepo { get; set; }
	ICountryRepo CountryRepo { get; set; }

    Task<int> SaveChangesAsync();
}
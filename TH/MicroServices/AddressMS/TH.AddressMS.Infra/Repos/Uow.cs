using TH.AddressMS.App;

namespace TH.AddressMS.Infra;

public class Uow : IUow
{
    private readonly AddressDbContext _dbContext;
    
	public IAddressRepo AddressRepo { get; set; }
	public ICountryRepo CountryRepo { get; set; }

    public Uow(AddressDbContext dbContext, IAddressRepo addressRepo, ICountryRepo countryRepo)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        
			AddressRepo = addressRepo ?? throw new ArgumentNullException(nameof(addressRepo));
			CountryRepo = countryRepo ?? throw new ArgumentNullException(nameof(countryRepo));
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
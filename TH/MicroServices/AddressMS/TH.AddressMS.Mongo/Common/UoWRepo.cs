namespace TH.AddressMS.Mongo
{
    public class UoWRepo : IUoWRepo
    {
        private readonly MongoDbContext _dbContext;
        public IAddressRepository AddressRepo { get ; set ; }
        public ICountryRepository CountryRepo { get; set; }
       
        public UoWRepo(MongoDbContext dbContext, IAddressRepository addressRepo, ICountryRepository countryRepo)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            AddressRepo = addressRepo ?? throw new ArgumentNullException(nameof(addressRepo));
            CountryRepo = countryRepo ?? throw new ArgumentNullException(nameof(countryRepo));            
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            ;
        }
    }
}

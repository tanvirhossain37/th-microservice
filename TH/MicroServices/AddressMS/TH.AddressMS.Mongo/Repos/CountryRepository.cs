using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.Mongo
{
    public class CountryRepository : Repo<Country>, ICountryRepository
    {
        public CountryRepository(MongoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
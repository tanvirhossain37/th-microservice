using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.Mongo
{
    public class AddressRepository : Repo<Address>, IAddressRepository
    {
        public AddressRepository(MongoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
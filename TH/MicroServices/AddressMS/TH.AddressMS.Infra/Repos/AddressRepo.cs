using MongoRepo.Context;
using MongoRepo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.Infra
{
    public class AddressRepo : CommonRepository<Address>, IAddressRepo
    {
        public AddressRepo(AddressDbContext dbContext) : base(dbContext)
        {
        }
    }
}
using MongoRepo.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.Core;

namespace TH.AddressMS.App
{
    public interface IAddressRepo : ICommonRepository<Address>
    {
    }
}
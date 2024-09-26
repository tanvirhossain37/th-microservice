using TH.AddressMS.Core;
using TH.AddressMS.Infra;
using TH.Common.Model;
using TH.Repo;

namespace TH.AddressMS.App;

public partial class AddressRepo : RepoSQL<Address>, IAddressRepo
{
    public AddressRepo(AddressDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }


}
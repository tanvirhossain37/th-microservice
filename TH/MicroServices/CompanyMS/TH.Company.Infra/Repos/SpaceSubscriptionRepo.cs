using TH.CompanyMS.Core;
using TH.CompanyMS.Infra;
using TH.Common.Model;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class SpaceSubscriptionRepo : RepoSQL<SpaceSubscription>, ISpaceSubscriptionRepo
{
    public SpaceSubscriptionRepo(CompanyDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }


}
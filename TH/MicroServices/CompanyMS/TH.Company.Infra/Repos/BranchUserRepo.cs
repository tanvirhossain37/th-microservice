using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.CompanyMS.Infra;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class BranchUserRepo : RepoSQL<BranchUser>, IBranchUserRepo
{
    public BranchUserRepo(CompanyDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }


}
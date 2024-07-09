using Microsoft.EntityFrameworkCore;
using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class BranchUserRepo : RepoSQL<BranchUser>, IBranchUserRepo
{
    public BranchUserRepo(DbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }


}
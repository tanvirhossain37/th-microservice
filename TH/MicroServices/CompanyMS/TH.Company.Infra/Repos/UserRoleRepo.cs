using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.CompanyMS.Infra;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class UserRoleRepo : RepoSQL<UserRole>, IUserRoleRepo
{
    public UserRoleRepo(CompanyDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }


}
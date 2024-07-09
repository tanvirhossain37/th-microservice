using Microsoft.EntityFrameworkCore;
using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class UserRoleRepo : RepoSQL<UserRole>, IUserRoleRepo
{
    public UserRoleRepo(DbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }


}
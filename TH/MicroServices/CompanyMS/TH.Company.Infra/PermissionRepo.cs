using Microsoft.EntityFrameworkCore;
using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class PermissionRepo : RepoSQL<Permission>, IPermissionRepo
{
    public PermissionRepo(DbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }


}
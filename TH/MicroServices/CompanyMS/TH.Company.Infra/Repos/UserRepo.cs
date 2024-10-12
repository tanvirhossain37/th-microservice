using TH.CompanyMS.Core;
using TH.CompanyMS.Infra;
using TH.Common.Model;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class UserRepo : RepoSQL<User>, IUserRepo
{
    public UserRepo(CompanyDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }

    public async Task<User> FindByUserNameAsync(string spaceId, string companyId, string userName, DataFilter dataFilter)
    {
        spaceId = string.IsNullOrWhiteSpace(spaceId) ? throw new ArgumentNullException(nameof(spaceId)) : spaceId.Trim().ToLower();
        companyId = string.IsNullOrWhiteSpace(companyId) ? throw new ArgumentNullException(nameof(companyId)) : companyId.Trim().ToLower();
        userName = string.IsNullOrWhiteSpace(userName) ? throw new ArgumentNullException(nameof(userName)) : userName.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.UserName.Equals(userName) &&
             e.SpaceId.Equals(spaceId) &&
             e.CompanyId.Equals(companyId)), dataFilter);
    }

    public async Task<User> FindByUserNameExceptMeAsync(string id, string spaceId, string companyId, string userName, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        spaceId = string.IsNullOrWhiteSpace(spaceId) ? throw new ArgumentNullException(nameof(spaceId)) : spaceId.Trim().ToLower();
        companyId = string.IsNullOrWhiteSpace(companyId) ? throw new ArgumentNullException(nameof(companyId)) : companyId.Trim().ToLower();
        userName = string.IsNullOrWhiteSpace(userName) ? throw new ArgumentNullException(nameof(userName)) : userName.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id)) &&
            (e.UserName.Equals(userName) &&
             e.SpaceId.Equals(spaceId) &&
             e.CompanyId.Equals(companyId)), dataFilter);
    }
}
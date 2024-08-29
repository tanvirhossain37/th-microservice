using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.CompanyMS.Infra;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class UserRepo : RepoSQL<User>, IUserRepo
{
    public UserRepo(CompanyDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }

    public async Task<User> FindByUserNameAsync(string userName, DataFilter dataFilter)
    {
        userName = string.IsNullOrWhiteSpace(userName) ? throw new ArgumentNullException(nameof(userName)) : userName.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e => (e.UserName.Equals(userName)), dataFilter);
    }

    public async Task<User> FindByUserNameExceptMeAsync(string id, string userName, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        userName = string.IsNullOrWhiteSpace(userName) ? throw new ArgumentNullException(nameof(userName)) : userName.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id)) &&
            (e.Name.Equals(userName)), dataFilter);
    }
}
using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public interface IUserRepo : IRepoSQL<User>
{
    Task<User> FindByUserNameAsync(string userName, DataFilter dataFilter);
    Task<User> FindByUserNameExceptMeAsync(string id, string userName, DataFilter dataFilter);
}
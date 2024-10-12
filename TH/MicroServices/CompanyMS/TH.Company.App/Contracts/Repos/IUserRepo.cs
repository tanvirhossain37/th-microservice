using TH.CompanyMS.Core;
using TH.Common.Model;
using TH.Repo;

namespace TH.CompanyMS.App;

public interface IUserRepo : IRepoSQL<User>
{
    Task<User> FindByUserNameAsync(string spaceId, string companyId, string userName, DataFilter dataFilter);
    Task<User> FindByUserNameExceptMeAsync(string id, string spaceId, string companyId, string userName, DataFilter dataFilter);
}
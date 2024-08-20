using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public interface IUserRepo : IRepoSQL<User>
{   
	Task<User> FindByNameAsync(string spaceId, string companyId, string userName, DataFilter dataFilter);
	Task<User> FindByNameExceptMeAsync(string id, string spaceId, string companyId, string userName, DataFilter dataFilter);
    
}
using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public interface IRoleRepo : IRepoSQL<Role>
{   
	Task<Role> FindByNameAsync(string spaceId, string companyId, string name, DataFilter dataFilter);
	Task<Role> FindByNameExceptMeAsync(string id, string spaceId, string companyId, string name, DataFilter dataFilter);
    
	Task<Role> FindByCodeAsync(string spaceId, string companyId, string code, DataFilter dataFilter);
	Task<Role> FindByCodeExceptMeAsync(string id, string spaceId, string companyId, string code, DataFilter dataFilter);
}
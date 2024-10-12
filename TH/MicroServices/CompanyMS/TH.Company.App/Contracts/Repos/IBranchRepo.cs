using TH.CompanyMS.Core;
using TH.Common.Model;
using TH.Repo;

namespace TH.CompanyMS.App;

public interface IBranchRepo : IRepoSQL<Branch>
{   
	Task<Branch> FindByNameAsync(string spaceId, string companyId, string name, DataFilter dataFilter);
	Task<Branch> FindByNameExceptMeAsync(string id, string spaceId, string companyId, string name, DataFilter dataFilter);
    
	Task<Branch> FindByCodeAsync(string spaceId, string companyId, string code, DataFilter dataFilter);
	Task<Branch> FindByCodeExceptMeAsync(string id, string spaceId, string companyId, string code, DataFilter dataFilter);
}
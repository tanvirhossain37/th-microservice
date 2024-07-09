using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public interface ICompanyRepo : IRepoSQL<Company>
{   
	Task<Company> FindByNameAsync(string spaceId, string name, DataFilter dataFilter);
	Task<Company> FindByNameExceptMeAsync(string id, string spaceId, string name, DataFilter dataFilter);
    
	Task<Company> FindByCodeAsync(string spaceId, string code, DataFilter dataFilter);
	Task<Company> FindByCodeExceptMeAsync(string id, string spaceId, string code, DataFilter dataFilter);
}
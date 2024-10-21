using TH.CompanyMS.Core;
using TH.Common.Model;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial interface IModuleRepo : IRepoSQL<Module>
{   
	Task<Module> FindByNameAsync(string name, DataFilter dataFilter);
	Task<Module> FindByNameExceptMeAsync(string id, string name, DataFilter dataFilter);
    
}
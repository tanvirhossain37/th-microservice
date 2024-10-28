using TH.AddressMS.Core;
using TH.Common.Model;
using TH.Repo;

namespace TH.AddressMS.App;

public partial interface ICountryRepo : IRepoSQL<Country>
{   
	Task<Country> FindByNameAsync(string name, DataFilter dataFilter);
	Task<Country> FindByNameExceptMeAsync(string id, string name, DataFilter dataFilter);
    
	Task<Country> FindByCodeAsync(string code, DataFilter dataFilter);
	Task<Country> FindByCodeExceptMeAsync(string id, string code, DataFilter dataFilter);
}
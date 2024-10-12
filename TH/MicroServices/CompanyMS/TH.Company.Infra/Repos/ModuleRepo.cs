using TH.CompanyMS.Core;
using TH.CompanyMS.Infra;
using TH.Common.Model;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class ModuleRepo : RepoSQL<Module>, IModuleRepo
{
    public ModuleRepo(CompanyDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }
    public async Task<Module> FindByNameAsync(string name, DataFilter dataFilter)
    {
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.Name.Equals(name)), dataFilter);
    }

    public async Task<Module> FindByNameExceptMeAsync(string id, string name, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id)) &&
            (e.Name.Equals(name)), dataFilter);
    }

}
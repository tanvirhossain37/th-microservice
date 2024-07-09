using Microsoft.EntityFrameworkCore;
using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class ModuleRepo : RepoSQL<Module>, IModuleRepo
{
    public ModuleRepo(DbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }
    public async Task<Module> FindByNameAsync(string name, DataFilter dataFilter)
    {
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }

    public async Task<Module> FindByNameExceptMeAsync(string id, string name, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }
    public async Task<Module> FindByCodeAsync(string code, DataFilter dataFilter)
    {
        code = string.IsNullOrWhiteSpace(code) ? throw new ArgumentNullException(nameof(code)) : code.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }

    public async Task<Module> FindByCodeExceptMeAsync(string id, string code, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        code = string.IsNullOrWhiteSpace(code) ? throw new ArgumentNullException(nameof(code)) : code.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }
}
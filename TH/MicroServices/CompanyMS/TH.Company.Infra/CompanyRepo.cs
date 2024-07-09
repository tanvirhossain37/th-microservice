using Microsoft.EntityFrameworkCore;
using TH.Common.Model;
using TH.CompanyMS.Core;
using TH.Repo;

namespace TH.CompanyMS.App;

public partial class CompanyRepo : RepoSQL<Company>, ICompanyRepo
{
    public CompanyRepo(DbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }
    public async Task<Company> FindByNameAsync(string spaceId, string name, DataFilter dataFilter)
    {
        spaceId = string.IsNullOrWhiteSpace(spaceId) ? throw new ArgumentNullException(nameof(spaceId)) : spaceId.Trim().ToLower();
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.SpaceId.Equals(spaceId, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }

    public async Task<Company> FindByNameExceptMeAsync(string id, string spaceId, string name, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        spaceId = string.IsNullOrWhiteSpace(spaceId) ? throw new ArgumentNullException(nameof(spaceId)) : spaceId.Trim().ToLower();
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.SpaceId.Equals(spaceId, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }
    public async Task<Company> FindByCodeAsync(string spaceId, string code, DataFilter dataFilter)
    {
        spaceId = string.IsNullOrWhiteSpace(spaceId) ? throw new ArgumentNullException(nameof(spaceId)) : spaceId.Trim().ToLower();
        code = string.IsNullOrWhiteSpace(code) ? throw new ArgumentNullException(nameof(code)) : code.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.SpaceId.Equals(spaceId, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }

    public async Task<Company> FindByCodeExceptMeAsync(string id, string spaceId, string code, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        spaceId = string.IsNullOrWhiteSpace(spaceId) ? throw new ArgumentNullException(nameof(spaceId)) : spaceId.Trim().ToLower();
        code = string.IsNullOrWhiteSpace(code) ? throw new ArgumentNullException(nameof(code)) : code.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase)) &&
            (e.SpaceId.Equals(spaceId, StringComparison.InvariantCultureIgnoreCase)), dataFilter);
    }
}
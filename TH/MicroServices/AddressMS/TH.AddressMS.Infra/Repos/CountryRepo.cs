using TH.AddressMS.Core;
using TH.AddressMS.Infra;
using TH.Common.Model;
using TH.Repo;

namespace TH.AddressMS.App;

public partial class CountryRepo : RepoSQL<Country>, ICountryRepo
{
    public CountryRepo(AddressDbContext dbContext, ICustomSort customSort) : base(dbContext, customSort)
    {
    }
    public async Task<Country> FindByNameAsync(string name, DataFilter dataFilter)
    {
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.Name.Equals(name)), dataFilter);
    }

    public async Task<Country> FindByNameExceptMeAsync(string id, string name, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentNullException(nameof(name)) : name.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id)) &&
            (e.Name.Equals(name)), dataFilter);
    }
    public async Task<Country> FindByIsoCodeAsync(string isoCode, DataFilter dataFilter)
    {
        isoCode = string.IsNullOrWhiteSpace(isoCode) ? throw new ArgumentNullException(nameof(isoCode)) : isoCode.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (e.IsoCode.Equals(isoCode)), dataFilter);
    }

    public async Task<Country> FindByCodeExceptMeAsync(string id, string code, DataFilter dataFilter)
    {
        id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentNullException(nameof(id)) : id.Trim().ToLower();
        code = string.IsNullOrWhiteSpace(code) ? throw new ArgumentNullException(nameof(code)) : code.Trim().ToLower();

        return await SingleOrDefaultQueryableAsync(e =>
            (!e.Id.Equals(id)) &&
            (e.Code.Equals(code)), dataFilter);
    }
}
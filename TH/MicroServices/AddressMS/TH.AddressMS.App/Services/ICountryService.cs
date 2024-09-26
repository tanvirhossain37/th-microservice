using TH.AddressMS.Core;
using TH.Common.Model;

namespace TH.AddressMS.App;

public interface ICountryService : IBaseService
{
    Task<ExcelResult> TryInitAsync(string path);
    Task<Country> SaveAsync(Country entity, DataFilter dataFilter, bool commit = true);
    Task<Country> UpdateAsync(Country entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(Country entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Country entity, DataFilter dataFilter, bool commit = true);
    Task<Country> FindByIdAsync(CountryFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Country>> GetAsync(CountryFilterModel filter, DataFilter dataFilter);
}
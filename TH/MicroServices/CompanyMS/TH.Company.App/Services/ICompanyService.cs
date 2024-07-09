using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface ICompanyService : IBaseService
{
    Task<Company> SaveAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<Company> UpdateAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task DeleteAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<Company> FindByIdAsync(CompanyFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Company>> GetAsync(CompanyFilterModel filter, DataFilter dataFilter);
}
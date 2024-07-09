using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface ICompanyService : IBaseService
{
    Task<Company> SaveAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<Company> UpdateAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<Company> FindAsync(CompanyFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Company>> GetAsync(CompanyFilterModel filter, DataFilter dataFilter);
}
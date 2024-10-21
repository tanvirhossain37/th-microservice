using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial interface ICompanyService : IBaseService
{
    Task<Company> SaveAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<Company> UpdateAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Company entity, DataFilter dataFilter, bool commit = true);
    Task<Company> FindByIdAsync(CompanyFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Company>> GetAsync(CompanyFilterModel filter, DataFilter dataFilter);
}
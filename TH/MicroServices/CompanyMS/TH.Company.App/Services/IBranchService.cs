using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial interface IBranchService : IBaseService
{
    Task<Branch> SaveAsync(Branch entity, DataFilter dataFilter, bool commit = true);
    Task<Branch> UpdateAsync(Branch entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(Branch entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Branch entity, DataFilter dataFilter, bool commit = true);
    Task<Branch> FindByIdAsync(BranchFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Branch>> GetAsync(BranchFilterModel filter, DataFilter dataFilter);
}
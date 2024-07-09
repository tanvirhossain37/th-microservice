using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface IBranchService : IBaseService
{
    Task<Branch> SaveAsync(Branch entity, DataFilter dataFilter, bool commit = true);
    Task<Branch> UpdateAsync(Branch entity, DataFilter dataFilter, bool commit = true);
    Task DeleteAsync(Branch entity, DataFilter dataFilter, bool commit = true);
    Task<Branch> FindByIdAsync(BranchFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Branch>> GetAsync(BranchFilterModel filter, DataFilter dataFilter);
}
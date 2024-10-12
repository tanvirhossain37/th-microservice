using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public interface IBranchUserService : IBaseService
{
    Task<BranchUser> SaveAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<BranchUser> UpdateAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<BranchUser> FindByIdAsync(BranchUserFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<BranchUser>> GetAsync(BranchUserFilterModel filter, DataFilter dataFilter);
}
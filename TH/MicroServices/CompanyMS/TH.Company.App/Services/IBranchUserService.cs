using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface IBranchUserService : IBaseService
{
    Task<BranchUser> SaveAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<BranchUser> UpdateAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(BranchUser entity, DataFilter dataFilter, bool commit = true);
    Task<BranchUser> FindAsync(BranchUserFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<BranchUser>> GetAsync(BranchUserFilterModel filter, DataFilter dataFilter);
}
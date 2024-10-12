using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public interface IRoleService : IBaseService
{
    Task<Role> SaveAsync(Role entity, DataFilter dataFilter, bool commit = true);
    Task<Role> UpdateAsync(Role entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(Role entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Role entity, DataFilter dataFilter, bool commit = true);
    Task<Role> FindByIdAsync(RoleFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Role>> GetAsync(RoleFilterModel filter, DataFilter dataFilter);
}
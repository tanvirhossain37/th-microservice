using TH.Common.Model;
using TH.CompanyMS.Core;

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
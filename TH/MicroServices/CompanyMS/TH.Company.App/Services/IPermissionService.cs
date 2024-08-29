using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface IPermissionService : IBaseService
{
    Task<Permission> SaveAsync(Permission entity, DataFilter dataFilter, bool commit = true);
    Task<Permission> UpdateAsync(Permission entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(Permission entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(Permission entity, DataFilter dataFilter, bool commit = true);
    Task<Permission> FindByIdAsync(PermissionFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<Permission>> GetAsync(PermissionFilterModel filter, DataFilter dataFilter);
}
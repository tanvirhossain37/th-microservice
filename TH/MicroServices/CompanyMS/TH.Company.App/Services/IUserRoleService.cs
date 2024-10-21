using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial interface IUserRoleService : IBaseService
{
    Task<UserRole> SaveAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<UserRole> UpdateAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<UserRole> FindByIdAsync(UserRoleFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<UserRole>> GetAsync(UserRoleFilterModel filter, DataFilter dataFilter);
}
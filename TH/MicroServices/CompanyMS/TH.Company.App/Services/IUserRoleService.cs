using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface IUserRoleService : IBaseService
{
    Task<UserRole> SaveAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<UserRole> UpdateAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<bool> SoftDeleteAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(UserRole entity, DataFilter dataFilter, bool commit = true);
    Task<UserRole> FindAsync(UserRoleFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<UserRole>> GetAsync(UserRoleFilterModel filter, DataFilter dataFilter);
}
using TH.CompanyMS.Core;
using TH.Common.Model;

namespace TH.CompanyMS.App;

public partial interface IUserService : IBaseService
{
    Task<User> SaveAsync(User entity, DataFilter dataFilter, bool commit = true);
    Task<User> UpdateAsync(User entity, DataFilter dataFilter, bool commit = true);
    Task<bool> ArchiveAsync(User entity, DataFilter dataFilter, bool commit = true);
    Task<bool> DeleteAsync(User entity, DataFilter dataFilter, bool commit = true);
    Task<User> FindByIdAsync(UserFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<User>> GetAsync(UserFilterModel filter, DataFilter dataFilter);
}
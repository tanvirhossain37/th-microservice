using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App;

public interface IUserService : IBaseService
{
    Task<User> SaveAsync(User entity, DataFilter dataFilter, bool commit = true);
    Task<User> UpdateAsync(User entity, DataFilter dataFilter, bool commit = true);
    Task DeleteAsync(User entity, DataFilter dataFilter, bool commit = true);
    Task<User> FindByIdAsync(UserFilterModel filter, DataFilter dataFilter);
    Task<IEnumerable<User>> GetAsync(UserFilterModel filter, DataFilter dataFilter);
}
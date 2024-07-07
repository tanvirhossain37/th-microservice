using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;
using TH.UserSvc.Core;

namespace TH.UserSvc.App
{
    public interface IUserService : IBaseService
    {
        Task<User> SaveAsync(User entity, DataFilter dataFilter);
        Task<User> UpdateAsync(User entity, DataFilter dataFilter);
        Task DeleteAsync(User entity, DataFilter dataFilter);
        Task<User> FindByIdAsync(string id, DataFilter dataFilter);
        Task<IEnumerable<User>> GetAsync(UserFilterModel filter, DataFilter dataFilter);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.UserSvc.Core;

namespace TH.UserSvc.App
{
    public interface IUserService : IBaseService
    {
        Task<User> SaveAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task DeleteAsync(User entity);
        Task<User> FindByIdAsync(long id);
        Task<IEnumerable<User>> GetAsync(UserFilterModel filter);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.UserSvc.Core;

namespace TH.UserSvc.App
{
    public class UserService : BaseService, IUserService
    {
        public UserService()
        {

        }

        public Task<User> SaveAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAsync(UserFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            ;
        }
    }
}
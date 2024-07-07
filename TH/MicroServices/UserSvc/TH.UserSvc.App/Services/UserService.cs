using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;
using TH.AddressMS.App.Interfaces;
using TH.UserSvc.Core;

namespace TH.UserSvc.App
{
    public class UserService : BaseService, IUserService
    {
        public IUoWRepo Repo { get; set; }

        public UserService(IUoWRepo repo)
        {
            Repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public Task<User> SaveAsync(User entity, DataFilter dataFilter)
        {
            throw new NotImplementedException();

        }

        public Task<User> UpdateAsync(User entity, DataFilter dataFilter)
        {
            throw new NotImplementedException();
        }

        public Task<User> FindByIdAsync(string id, DataFilter dataFilter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAsync(UserFilterModel filter, DataFilter dataFilter)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity, DataFilter dataFilter)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            Repo?.Dispose();
        }
    }
}
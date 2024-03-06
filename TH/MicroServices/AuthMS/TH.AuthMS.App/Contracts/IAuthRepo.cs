using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public interface IAuthRepo : IDisposable
    {
        Task<bool> SaveAsync(User entity, string password);
        Task<User> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<SignInViewModel> GenerateToken(SignInInputModel entity);
    }
}
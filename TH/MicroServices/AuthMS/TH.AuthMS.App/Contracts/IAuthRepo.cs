using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public interface IAuthRepo : IDisposable
    {
        Task<bool> SaveAsync(User identityUser, string password);
        Task<IdentityResult> UpdateAsync(User identityUser);
        Task<User> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(User user, string password);
        SignInViewModel GenerateToken(string userName);
        string GenerateRefreshToken();
        ClaimsPrincipal GetTokenPrincipal(string token);
    }
}
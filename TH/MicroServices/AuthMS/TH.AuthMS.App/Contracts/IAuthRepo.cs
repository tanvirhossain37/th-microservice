using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TH.AddressMS.App;

namespace TH.AuthMS.App
{
    public interface IAuthRepo : IDisposable
    {
        Task<bool> SaveAsync(ApplicationUser identityApplicationUser, string password);
        Task<IdentityResult> UpdateAsync(ApplicationUser identityApplicationUser);
        Task<ApplicationUser> FindByUserNameAsync(string userName);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);
        SignInViewModel GenerateToken(ApplicationUser identityApplicationUser);
        string GenerateRefreshToken();
        ClaimsPrincipal GetTokenPrincipal(string token);
        Task<ApplicationUser> ActivateAccountAsync(ActivationCodeInputModel model);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser identityUser);
        Task<bool> UpdatePasswordAsync(ForgotPasswordInputModel model);
    }
}
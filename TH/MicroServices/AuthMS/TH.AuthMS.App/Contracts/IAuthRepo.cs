using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
        Task<bool> ResetPasswordAsync(ForgotPasswordInputModel model);
    }
}

using System.Net;
using TH.AuthMS.App;
using TH.Common.Model;

namespace TH.AuthMS.App
{
    public interface IAuthService : IBaseService, IDisposable
    {
        Task<SignUpViewModel> SignUpAsync(SignUpInputModel entity, DataFilter dataFilter);
        Task<SignInViewModel> SignInAsync(SignInInputModel entity, DataFilter dataFilter);
        Task<SignInViewModel> RefreshToken(RefreshTokenInputModel model, DataFilter dataFilter);
        Task<bool> ActivateAccountAsync(ActivationCodeInputModel model, DataFilter dataFilter);
        Task<bool> ResendActivationCodeAsync(ResendActivationCodeInputModel model, DataFilter dataFilter);
        Task ForgotPasswordAsync(ForgotPasswordInputModel model, DataFilter dataFilter);
        Task<bool> ResetPasswordAsync(ForgotPasswordInputModel model, DataFilter dataFilter);
        Task<ApplicationUser> FindByEmailAsync(ApplicationUserFilterModel filter, DataFilter dataFilter);
    }
}
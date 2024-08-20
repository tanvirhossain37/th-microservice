
using System.Net;
using TH.AuthMS.App;
using TH.Common.Model;

namespace TH.AuthMS.App
{
    public interface IAuthService :IBaseService, IDisposable
    {
        public CompanyGrpcClientService GrpcClientService { get; set; }
        Task<SignUpViewModel> SignUpAsync(SignUpInputModel entity);
        Task<SignInViewModel> SignInAsync(SignInInputModel entity);
        Task<SignInViewModel> RefreshToken(RefreshTokenInputModel model);
        Task<bool> ActivateAccountAsync(ActivationCodeInputModel model);
        Task ForgotPasswordAsync(ForgotPasswordInputModel model);
        Task<bool> UpdatePasswordAsync(ForgotPasswordInputModel model);
    }
}
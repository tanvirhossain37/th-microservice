
using System.Net;
using TH.AddressMS.App;

namespace TH.AuthMS.App
{
    public interface IAuthService : IDisposable
    {
        Task<bool> SignUpAsync(SignUpInputModel entity);
        Task<SignInViewModel> SignInAsync(SignInInputModel entity);
        Task<SignInViewModel> RefreshToken(RefreshTokenInputModel model);
        Task<bool> ActivateAccountAsync(ActgivationCodeInputModel model);
    }
}
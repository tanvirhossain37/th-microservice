
using System.Net;

namespace TH.AuthMS.App
{
    public interface IAuthService : IDisposable
    {
        Task<bool> SignUpAsync(SignUpInputModel entity);
        Task<SignInViewModel> SignInAsync(SignInInputModel entity);
        Task<SignInViewModel> RefreshToken(RefreshTokenInputModel model);
    }
}
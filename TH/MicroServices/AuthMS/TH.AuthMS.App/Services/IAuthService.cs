
using System.Net;
using TH.AddressMS.App;
using TH.AuthMS.App.GrpcServices;

namespace TH.AuthMS.App
{
    public interface IAuthService : IDisposable
    {
        public CompanyGrpcClientService GrpcClientService { get; set; }
        Task<bool> SignUpAsync(SignUpInputModel entity);
        Task<SignInViewModel> SignInAsync(SignInInputModel entity);
        Task<SignInViewModel> RefreshToken(RefreshTokenInputModel model);
        Task<bool> ActivateAccountAsync(ActgivationCodeInputModel model);
    }
}
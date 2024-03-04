
namespace TH.AuthMS.App
{
    public interface IAuthService : IDisposable
    {
        Task<bool> SignUpAsync(SignUpInputModel entity);
    }
}
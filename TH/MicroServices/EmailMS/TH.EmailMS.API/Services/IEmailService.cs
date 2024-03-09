namespace TH.EmailMS.API
{
    public interface IEmailService : IDisposable
    {
        Task<bool> SendEmailAsync(EmailInputModel model);
    }
}
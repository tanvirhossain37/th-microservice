using System.Net.Mail;

namespace TH.EmailMS.API.Infra
{
    public interface IEmailRepo : IDisposable
    {
        Task<bool> SendEmailAsync(MailMessage message);
    }
}
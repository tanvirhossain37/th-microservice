using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using TH.EmailMS.API.Infra;

namespace TH.EmailMS.API
{
    public class EmailRepo : IEmailRepo
    {
        private readonly IConfiguration _config;
        private readonly SmtpClient _smtpClient;

        public EmailRepo(IConfiguration config, SmtpClient smtpClient)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
           _smtpClient = smtpClient ?? throw new ArgumentNullException(nameof(smtpClient));
        }

        public async Task<bool> SendEmailAsync(MailMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            try
            {
                _smtpClient.Host = _config.GetSection("Email:Host").Value;
                _smtpClient.Port = Convert.ToInt32(_config.GetSection("Email:Port").Value);
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.EnableSsl = true;
                _smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(_config.GetSection("Email:Username").Value,
                    _config.GetSection("Email:Password").Value);

                //await Task.Run(() => _smtpClient.Send(message));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            _smtpClient?.Dispose();
        }
    }
}
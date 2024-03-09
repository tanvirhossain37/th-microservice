using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Mail;
using TH.Common.Lang;
using TH.Common.Util;
using TH.EmailMS.API.Infra;

namespace TH.EmailMS.API
{
    public class EmailService : IEmailService
    {
        private readonly IEmailRepo _emailRepo;
        private readonly IConfiguration _config;

        public EmailService(IEmailRepo emailRepo, IConfiguration config)
        {
            _emailRepo = emailRepo ?? throw new ArgumentNullException(nameof(emailRepo));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<bool> SendEmailAsync(EmailInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            ApplyValidationBL(model);

            var mailAddress = new MailAddress(_config.GetSection("Email:FromEmail").Value,
                _config.GetSection("Email:FromName").Value);

            var message = new MailMessage();
            message.Subject = model.Subject;
            message.From = mailAddress;
            message.Body = model.Content;
            message.IsBodyHtml = true;

            foreach (var email in model.To) message.To.Add(email);
            foreach (var email in model.Cc) message.CC.Add(email);
            foreach (var email in model.Bcc) message.Bcc.Add(email);
            //foreach (var attachment in model.Attachments) message.Attachments.Add(attachment);


            return await _emailRepo.SendEmailAsync(message);
        }

        private void ApplyValidationBL(EmailInputModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            if ((model.To is null) || (model.To?.Count <= 0)) throw new ValidationException(Lang.Find("error_validation"));
            foreach (var email in model.To)
                if (!Util.TryIsValidEmail(email))
                    throw new ValidationException();

            if ((model.Cc is null) || (model.Cc?.Count < 0)) model.Cc = new List<string>();
            foreach (var email in model.Cc)
                if (!Util.TryIsValidEmail(email))
                    throw new ValidationException();

            if ((model.Bcc is null) || (model.Bcc?.Count < 0)) model.Bcc = new List<string>();
            foreach (var email in model.Bcc)
                if (!Util.TryIsValidEmail(email))
                    throw new ValidationException();

            //if ((model.Attachments is null) || (model.Attachments?.Count < 0)) model.Attachments = new List<Attachment>();
            //foreach (var attachment in model.Attachments)
            //{
            //    attachment.Name = string.IsNullOrWhiteSpace(attachment.Name)
            //        ? throw new ValidationException(Lang.Find("error_validation"))
            //        : attachment.Name.Trim();

            //    //should check others
            //}

            model.Subject = string.IsNullOrWhiteSpace(model.Subject) ? string.Empty : model.Subject.Trim();
            model.Content = string.IsNullOrWhiteSpace(model.Content) ? string.Empty : model.Content.Trim();
        }

        public void Dispose()
        {
            _emailRepo?.Dispose();
        }
    }
}
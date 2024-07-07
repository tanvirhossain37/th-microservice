using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using MongoDB.Driver.Core.Operations;
using TH.AddressMS.App;
using TH.AuthMS.App;
using TH.AuthMS.Core;
using TH.Common.Lang;
using TH.EventBus.Messages;
using TH.UserSvc.App;

namespace TH.AuthMS.App
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IConfiguration _config;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthService(IAuthRepo authRepo, IConfiguration config, IPublishEndpoint publishEndpoint)
        {
            _authRepo = authRepo ?? throw new ArgumentNullException(nameof(authRepo));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        public async Task<bool> SignUpAsync(SignUpInputModel entity)
        {
            ApplyValidationBl(entity);

            var user = new User
            {
                Name = entity.Name,
                UserName = entity.UserName,
                Email = entity.Email,
                UserTypeId = (int)UserTypeEnum.Owner,
                CreatedDate = DateTime.Now,
                ActivationCode = Guid.NewGuid().ToString(),
                CodeExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(_config.GetSection("ActivationCodeExpiryTime").Value))//1 day
            };

            var result= await _authRepo.SaveAsync(user, entity.Password);

            //send email
            //publish to eventbus
            var encodedCode = System.Web.HttpUtility.UrlEncode(user.ActivationCode);
            var verifyUrl = $"{_config.GetSection("GlobalConfigs:BaseUrl").Value}/verify?code={encodedCode}";

            var emailEvent = new EmailEvent();
            emailEvent.To.Add(user.Email);
            emailEvent.Subject = "Activate account please!";
            emailEvent.Content = string.Format(Lang.Find("email_new_user"), user.Name, verifyUrl);

            await _publishEndpoint.Publish(emailEvent);

            return result;
        }

        public async Task<SignInViewModel> SignInAsync(SignInInputModel entity)
        {
            var identityUser = await _authRepo.FindByUserNameAsync(entity.UserName);
            if (identityUser is null) throw new UnauthorizedAccessException(Lang.Find("error_invalidusername"));

            var isCorrectPassword = await _authRepo.CheckPasswordAsync(identityUser, entity.Password);
            if (!isCorrectPassword) throw new UnauthorizedAccessException(Lang.Find("error_wrongpassword"));

            //email confirmed?
            if(!identityUser.EmailConfirmed) throw new UnauthorizedAccessException(Lang.Find("error_emailnotconfirmed"));

            var signInViewModel = _authRepo.GenerateToken(entity.UserName);
            signInViewModel.RefreshToken = _authRepo.GenerateRefreshToken();
            signInViewModel.userName = identityUser.UserName;
            signInViewModel.Name = identityUser.Name;
            signInViewModel.Email = identityUser.Email;
            signInViewModel.EmailConfirmed = identityUser.EmailConfirmed;
            signInViewModel.UserTypeId = identityUser.UserTypeId;
            signInViewModel.CreatedDate = identityUser.CreatedDate;
            signInViewModel.ModifiedDate = identityUser.ModifiedDate;

            //update db
            identityUser.RefreshToken = signInViewModel.RefreshToken;
            identityUser.RefreshTokenExpiryTime = SetRefreshTokenExpiryTime();
            identityUser.ModifiedDate = DateTime.Now;

            var result = await _authRepo.UpdateAsync(identityUser);
            if (!result.Succeeded)
            {
                var code = result?.Errors?.FirstOrDefault()?.Code;
                throw new CustomException(Lang.Find($"error_{code}"));
            }

            //publish to eventbus
            var emailEvent = new EmailEvent();
            emailEvent.To.Add(identityUser.Email);
            emailEvent.Subject = "Security Alter";
            emailEvent.Content = $"{identityUser.UserName}, you got signed in at {DateTime.Now}";

            await _publishEndpoint.Publish(emailEvent);

            return signInViewModel;
        }

        public async Task<SignInViewModel> RefreshToken(RefreshTokenInputModel model)
        {
            var principal = _authRepo.GetTokenPrincipal(model.Token);
            if (principal?.Identity?.Name is null)
                throw new UnauthorizedAccessException(Lang.Find("error_unauthorized_access"));

            var identityUser = await _authRepo.FindByUserNameAsync(principal.Identity.Name);
            if (identityUser is null || identityUser.RefreshToken != model.RefreshToken ||
                DateTime.Now > identityUser.RefreshTokenExpiryTime)
                throw new UnauthorizedAccessException(Lang.Find("error_unauthorized"));

            var signInViewModel = _authRepo.GenerateToken(identityUser.UserName);
            signInViewModel.RefreshToken = _authRepo.GenerateRefreshToken();

            //update db
            identityUser.RefreshToken = signInViewModel.RefreshToken;
            identityUser.RefreshTokenExpiryTime = SetRefreshTokenExpiryTime();
            identityUser.ModifiedDate = DateTime.Now;

            var result = await _authRepo.UpdateAsync(identityUser);
            if (!result.Succeeded)
            {
                var code = result?.Errors?.FirstOrDefault()?.Code;
                throw new CustomException(Lang.Find($"error_{code}"));
            }

            return signInViewModel;
        }

        public async Task<bool> ActivateAccountAsync(ActgivationCodeInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var identityUser = await _authRepo.ActivateAccountAsync(model);
            if (identityUser is null) throw new CustomException(Lang.Find("error_not_found"));

            if (identityUser.EmailConfirmed)
            {
                var emailEvent = new EmailEvent();
                emailEvent.To.Add(identityUser.Email);
                emailEvent.Subject = "Account activation confirmed!";
                emailEvent.Content = $"Welcome {identityUser.Name} on the space!";

                await _publishEndpoint.Publish(emailEvent);
            }

            return identityUser.EmailConfirmed;
        }

        public void Dispose()
        {
            _authRepo?.Dispose();
        }

        #region Business Logic

        private void ApplyValidationBl(SignUpInputModel entity)
        {
            entity.UserName = string.IsNullOrWhiteSpace(entity.UserName)
                ? throw new CustomException($"{Lang.Find("error_validation")} : UserName")
                : entity.UserName.Trim();
            entity.Password = string.IsNullOrWhiteSpace(entity.Password)
                ? throw new CustomException($"{Lang.Find("error_validation")} : Password")
                : entity.Password.Trim();
            entity.Email = string.IsNullOrWhiteSpace(entity.Email)
                ? throw new CustomException($"{Lang.Find("error_validation")} : Email")
                : entity.Email.Trim();
        }

        private DateTime SetRefreshTokenExpiryTime()
        {
            //update here to test
            return DateTime.Now.AddDays(Convert.ToDouble(_config.GetSection("Jwt:RefreshTokenExpiryTime").Value));
            //return DateTime.Now.AddMinutes(1);
        }

        #endregion
    }
}
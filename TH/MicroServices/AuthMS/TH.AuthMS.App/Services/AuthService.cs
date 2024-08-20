using AutoMapper;
using Microsoft.Extensions.Configuration;
using MassTransit;
using TH.Common.Lang;
using TH.EventBus.Messages;
using TH.Common.Model;
using TH.Common.Util;
using MongoDB.Driver;

namespace TH.AuthMS.App
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IConfiguration _config;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;
        public CompanyGrpcClientService GrpcClientService { get; set; }

        public AuthService(IAuthRepo authRepo, IConfiguration config, IPublishEndpoint publishEndpoint, IMapper mapper) : base(mapper,
            publishEndpoint)
        {
            _authRepo = authRepo ?? throw new ArgumentNullException(nameof(authRepo));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<SignUpViewModel> SignUpAsync(SignUpInputModel entity)
        {
            entity.UserName = string.IsNullOrWhiteSpace(entity.UserName) ? Util.TryGenerateUserName(entity.Name) : entity.UserName.Trim();

            ApplyValidationBl(entity);

            var identityUser = new ApplicationUser
            {
                Name = entity.Name,
                UserName = entity.UserName,
                Email = entity.Email,
                CreatedDate = DateTime.Now,
                ActivationCode = Util.TryGenerateCode(),
                //ReferralId = entity.ReferralId,
                CodeExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(_config.GetSection("ActivationCodeExpiryTime").Value)) //1 day
            };

            //self
            if (string.IsNullOrWhiteSpace(entity.ReferralId))
            {
                //duplicate check
                var existingEntity = await _authRepo.FindByEmailAsync(identityUser.Email);
                if (existingEntity is not null) throw new CustomException($"{Lang.Find("error_duplicate")}: Email");

                await _authRepo.SaveAsync(identityUser, entity.Password);
                await EmailCodeAsync(identityUser);
            }
            else
            {
                //invitation
                var referallUser = await _authRepo.FindByUserNameAsync(entity.ReferralId);
                if (referallUser == null) throw new CustomException(Lang.Find("data_notfound"));

                //duplicate check
                var existingEntity = await _authRepo.FindByEmailAsync(identityUser.Email);

                if (existingEntity is null)
                {
                    //signup link
                    var encodedCode = System.Web.HttpUtility.UrlEncode(identityUser.ActivationCode);
                    var verifyUrl = $"{_config.GetSection("GlobalConfigs:BaseUrl").Value}/signup?code={encodedCode}";

                    identityUser.ReferralId = entity.ReferralId;

                    await _authRepo.SaveAsync(identityUser, entity.Password);
                    await EmailLinkAsync(identityUser, referallUser.Name, verifyUrl);
                }
                else
                {
                    //signin link
                    existingEntity.ActivationCode = Util.TryGenerateCode();
                    existingEntity.CodeExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(_config.GetSection("ActivationCodeExpiryTime").Value)); //1 day

                    var encodedCode = System.Web.HttpUtility.UrlEncode(existingEntity.ActivationCode);
                    var verifyUrl = $"{_config.GetSection("GlobalConfigs:BaseUrl").Value}/signin?code={encodedCode}";

                    
                    
                    await _authRepo.UpdateAsync(existingEntity);
                    await EmailLinkAsync(existingEntity, referallUser.Name, verifyUrl);
                }
            }

            return _mapper.Map<ApplicationUser, SignUpViewModel>(identityUser);
        }

        public async Task<SignInViewModel> SignInAsync(SignInInputModel entity)
        {
            var identityUser = await _authRepo.FindByEmailAsync(entity.Email);
            if (identityUser is null) throw new UnauthorizedAccessException(Lang.Find("error_user"));

            var isCorrectPassword = await _authRepo.CheckPasswordAsync(identityUser, entity.Password);
            if (!isCorrectPassword) throw new UnauthorizedAccessException(Lang.Find("error_wrongpassword"));

            //email confirmed?
            if (!identityUser.EmailConfirmed)
            {
                //update user
                identityUser.ActivationCode = Util.TryGenerateCode();
                identityUser.CodeExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(_config.GetSection("ActivationCodeExpiryTime").Value)); //1 day

                var updateResult = await _authRepo.UpdateAsync(identityUser);
                if (!updateResult.Succeeded)
                {
                    var code = updateResult?.Errors?.FirstOrDefault()?.Code;
                    throw new CustomException(Lang.Find($"error_{code}"));
                }

                if (string.IsNullOrWhiteSpace(identityUser.ReferralId))
                {
                    //send code
                    //publish to eventbus
                    await EmailCodeAsync(identityUser);
                }
                else
                {
                    //send link
                    var encodedCode = System.Web.HttpUtility.UrlEncode(identityUser.ActivationCode);
                    var verifyUrl = $"{_config.GetSection("GlobalConfigs:BaseUrl").Value}/invitation?code={encodedCode}";

                    //publish to eventbus
                    await EmailLinkAsync(identityUser, "",verifyUrl);
                }

                throw new InactiveUserException(Lang.Find("error_emailnotconfirmed"));
            }

            var signInViewModel = _authRepo.GenerateToken(identityUser);
            signInViewModel.RefreshToken = _authRepo.GenerateRefreshToken();

            signInViewModel.Name = identityUser.Name;
            signInViewModel.Email = identityUser.Email;
            signInViewModel.EmailConfirmed = identityUser.EmailConfirmed;
            //signInViewModel.UserTypeId = identityUser.UserTypeId;
            signInViewModel.CreatedDate = identityUser.CreatedDate;
            signInViewModel.ModifiedDate = identityUser.ModifiedDate;

            //if (identityUser.UserTypeId == (int)UserTypeEnum.Owner)
            //{
            //    signInViewModel.SpaceId = identityUser.Id;
            //}

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

            //grpc service
            //var reply = await GrpcClientService.GetPermissions(new PermissionFilterRequest { SpaceId = "hello" });

            return signInViewModel;
        }

        private async Task EmailLinkAsync(ApplicationUser identityUser, string referralName, string verifyUrl)
        {
            var emailEvent = new EmailEvent();
            emailEvent.To.Add(identityUser.Email);
            emailEvent.Subject = "Invitation from We Space Inc.";
            emailEvent.Content = string.Format(Lang.Find("email_invitation"), identityUser.Email, referralName, verifyUrl);

            await _publishEndpoint.Publish(emailEvent);
        }

        private async Task EmailCodeAsync(ApplicationUser identityUser)
        {
            var emailEventAgain = new EmailEvent();
            emailEventAgain.To.Add(identityUser.Email);
            emailEventAgain.Subject = "Activate account please!";
            emailEventAgain.Content = string.Format(Lang.Find("email_new_user"), identityUser.Name, identityUser.ActivationCode);

            await _publishEndpoint.Publish(emailEventAgain);
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

            var signInViewModel = _authRepo.GenerateToken(identityUser);
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

        public async Task<bool> ActivateAccountAsync(ActivationCodeInputModel model)
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

        public async Task ForgotPasswordAsync(ForgotPasswordInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var identityUser = await _authRepo.FindByEmailAsync(model.Email);
            if (identityUser is null) throw new CustomException(Lang.Find("error_not_found"));

            //update user
            identityUser.ActivationCode = await _authRepo.GeneratePasswordResetTokenAsync(identityUser);
            identityUser.CodeExpiryTime = DateTime.Now.AddDays(Convert.ToDouble(_config.GetSection("ActivationCodeExpiryTime").Value)); //1 day

            var updateResult = await _authRepo.UpdateAsync(identityUser);
            if (!updateResult.Succeeded)
            {
                var code = updateResult?.Errors?.FirstOrDefault()?.Code;
                throw new CustomException(Lang.Find($"error_{code}"));
            }

            if (string.IsNullOrWhiteSpace(identityUser.ReferralId))
            {
                //send link with token
                var encodedCode = System.Web.HttpUtility.UrlEncode(identityUser.ActivationCode);
                var verifyUrl = $"{_config.GetSection("GlobalConfigs:BaseUrl").Value}/changepassword?token={encodedCode}";

                //publish to eventbus
                var emailEvent = new EmailEvent();
                emailEvent.To.Add(identityUser.Email);
                emailEvent.Subject = "Change Password Request";
                emailEvent.Content = string.Format(Lang.Find("change_password"), identityUser.Name, verifyUrl);

                await _publishEndpoint.Publish(emailEvent);
            }
            else
            {
                if (identityUser.EmailConfirmed)
                {
                    //send code
                    //publish to eventbus
                    await EmailCodeAsync(identityUser);
                }
                else
                {
                    //send link
                    var encodedCode = System.Web.HttpUtility.UrlEncode(identityUser.ActivationCode);
                    var verifyUrl = $"{_config.GetSection("GlobalConfigs:BaseUrl").Value}/invitation?code={encodedCode}";

                    //publish to eventbus
                    await EmailLinkAsync(identityUser,"", verifyUrl);
                }
            }
        }

        public async Task<bool> UpdatePasswordAsync(ForgotPasswordInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var succeed = await _authRepo.UpdatePasswordAsync(model);

            //update email confirmed
            if (succeed)
            {
                var identityUser = await _authRepo.FindByEmailAsync(model.Email);
                identityUser.EmailConfirmed = true;

                var result = await _authRepo.UpdateAsync(identityUser);
                if (!result.Succeeded)
                {
                    var code = result?.Errors?.FirstOrDefault()?.Code;
                    throw new CustomException(Lang.Find($"error_{code}"));
                }
            }

            return true;
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
            entity.Email = string.IsNullOrWhiteSpace(entity.Email)
                ? null
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
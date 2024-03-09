using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AuthMS.App;
using TH.AuthMS.Core;
using TH.Common.Lang;

namespace TH.AuthMS.App
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly IConfiguration _config;

        public AuthService(IAuthRepo authRepo, IConfiguration config)
        {
            _authRepo = authRepo;
            _config = config;
        }

        public async Task<bool> SignUpAsync(SignUpInputModel entity)
        {
            ApplyValidationBl(entity);

            var user = new User
            {
                UserName = entity.UserName,
                Email = entity.Email,
                UserTypeId = (int)UserTypeEnum.Owner,
                CreatedDate = DateTime.Now,
            };

            return await _authRepo.SaveAsync(user, entity.Password);
        }

        public async Task<SignInViewModel> SignInAsync(SignInInputModel entity)
        {
            var identityUser = await _authRepo.FindByUserNameAsync(entity.UserName);
            if (identityUser is null) throw new UnauthorizedAccessException(Lang.Find("error_invalidusername"));

            var isCorrectPassword = await _authRepo.CheckPasswordAsync(identityUser, entity.Password);
            if (!isCorrectPassword) throw new UnauthorizedAccessException(Lang.Find("error_wrongpassword"));

            var signInViewModel = _authRepo.GenerateToken(entity.UserName);
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
            return DateTime.Now.AddDays(Convert.ToDouble(_config.GetSection("Jwt:RefreshTokenExpiryTime").Value));
            //return DateTime.Now.AddMinutes(1);
        }

        #endregion
    }
}
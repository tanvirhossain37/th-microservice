using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AuthMS.App;
using TH.AuthMS.Core;

namespace TH.AuthMS.App
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;

        public AuthService(IAuthRepo authRepo)
        {
            _authRepo = authRepo;
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

        private void ApplyValidationBl(SignUpInputModel entity)
        {
            entity.UserName = string.IsNullOrWhiteSpace(entity.UserName) ? string.Empty : entity.UserName.Trim();
            entity.Password = string.IsNullOrWhiteSpace(entity.Password) ? string.Empty : entity.Password.Trim();
            entity.Email = string.IsNullOrWhiteSpace(entity.Email) ? string.Empty : entity.Email.Trim();
        }

        public void Dispose()
        {
            _authRepo?.Dispose();
        }
    }
}
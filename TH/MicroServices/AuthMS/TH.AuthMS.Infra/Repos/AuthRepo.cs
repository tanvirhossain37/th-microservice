using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TH.AuthMS.App;
using TH.Common.Lang;
using TH.Common.Model;

namespace TH.AuthMS.Infra
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<User>  _roleManager;
        private readonly IConfiguration _config;

        public AuthRepo(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            
            _config = config;
        }

        public async Task<bool> SaveAsync(ApplicationUser identityApplicationUser, string password)
        {
            var result = await _userManager.CreateAsync(identityApplicationUser, password);

            if (!result.Succeeded)
            {
                var code = result?.Errors?.FirstOrDefault()?.Code;
                throw new CustomException(Lang.Find($"error_{code}"));
            }

            return result.Succeeded;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser identityApplicationUser)
        {
            return await _userManager.UpdateAsync(identityApplicationUser);
        }

        public async Task DeleteAsync(ApplicationUser identityUser)
        {
            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));
            await _userManager.DeleteAsync(identityUser);
        }

        public async Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
        {
            return _userManager.CheckPasswordAsync(applicationUser, password);
        }

        public SignInViewModel GenerateToken(ApplicationUser identityApplicationUser)
        {
            var claims = new List<Claim>();

            //call permissions
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identityApplicationUser.UserName),
                new Claim(ClaimTypes.Email, identityApplicationUser.Email),
                new Claim("SpaceId", identityApplicationUser.Id),
                new Claim("FullName", identityApplicationUser.Name),
                new Claim(TS.Controllers.Company, TS.Permissions.Read),
                new Claim(TS.Controllers.Company, TS.Permissions.Write),
                new Claim(TS.Controllers.Company, TS.Permissions.Update),
                new Claim(TS.Controllers.Company, TS.Permissions.SoftDelete),
                new Claim(TS.Controllers.Company, TS.Permissions.Delete),
                new Claim(TS.Controllers.Permission, TS.Permissions.Read),
                new Claim(TS.Controllers.Permission, TS.Permissions.Write),
                new Claim(TS.Controllers.Permission, TS.Permissions.Update),
                new Claim(TS.Controllers.Permission, TS.Permissions.SoftDelete),
                new Claim(TS.Controllers.Permission, TS.Permissions.Delete),
                new Claim(TS.Controllers.Role, TS.Permissions.Read),
                new Claim(TS.Controllers.Role, TS.Permissions.Write),
                new Claim(TS.Controllers.Role, TS.Permissions.Update),
                new Claim(TS.Controllers.Role, TS.Permissions.SoftDelete),
                new Claim(TS.Controllers.Role, TS.Permissions.Delete),
                new Claim(TS.Controllers.User, TS.Permissions.Read),
                new Claim(TS.Controllers.User, TS.Permissions.Write),
                new Claim(TS.Controllers.User, TS.Permissions.Update),
                new Claim(TS.Controllers.User, TS.Permissions.SoftDelete),
                new Claim(TS.Controllers.User, TS.Permissions.Delete),
                new Claim(TS.Controllers.UserCompany, TS.Permissions.Read),
                new Claim(TS.Controllers.UserCompany, TS.Permissions.Write),
                new Claim(TS.Controllers.UserCompany, TS.Permissions.Update),
                new Claim(TS.Controllers.UserCompany, TS.Permissions.SoftDelete),
                new Claim(TS.Controllers.UserCompany, TS.Permissions.Delete)
            };


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            //update here to test
            var expires = DateTime.Now.AddMinutes(Convert.ToDouble(_config.GetSection("Jwt:TokenExpiryTime").Value));
            //var expires = DateTime.Now.AddSeconds(10);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new SignInViewModel { Token = token, TokenExpiryTime = expires };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetTokenPrincipal(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            var validation = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateLifetime = false,
                ValidateActor = false,
                ValidateIssuer = false,
                ValidateAudience = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        public async Task<ApplicationUser> ActivateAccountAsync(ActivationCodeInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var identityUser = _userManager.Users.SingleOrDefault(x=>x.ActivationCode.Equals(model.ActivateCode));
            if (identityUser is null) throw new CustomException(Lang.Find("error_not_found"));

            //time? util?
            if (identityUser.CodeExpiryTime >= DateTime.Now)
            {
                identityUser.EmailConfirmed = true;
                await _userManager.UpdateAsync(identityUser);
            }
            else
            {
                throw new CustomException(Lang.Find("error_activae_code_expired"));
            }

            return identityUser;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser identityUser)
        {
            if (identityUser == null) throw new ArgumentNullException(nameof(identityUser));

            return await _userManager.GeneratePasswordResetTokenAsync(identityUser);
        }

        public async Task<bool> ResetPasswordAsync(ForgotPasswordInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var identityUser = _userManager.Users.SingleOrDefault(x => x.ActivationCode.Equals(model.ActivationCode));
            if (identityUser is null) throw new CustomException(Lang.Find("error_not_found"));

            //time? util?
            if (identityUser.CodeExpiryTime >= DateTime.Now)
            {
                var result = await _userManager.ResetPasswordAsync(identityUser, identityUser.ResetPasswordToken, model.Password);

                if (!result.Succeeded)
                {
                    var code = result?.Errors?.FirstOrDefault()?.Code;
                    throw new CustomException(Lang.Find($"error_{code}"));
                }

                return result.Succeeded;
            }
            else
            {
                throw new CustomException(Lang.Find("error_activae_code_expired"));
            }
        }

        public void Dispose()
        {
            _userManager?.Dispose();
        }

        #region Business Logic

        #endregion
    }
}
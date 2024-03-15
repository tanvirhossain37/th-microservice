using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TH.AuthMS.App;
using TH.Common.Lang;

namespace TH.AuthMS.Infra
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthRepo(UserManager<User> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<bool> SaveAsync(User identityUser, string password)
        {
            var result = await _userManager.CreateAsync(identityUser, password);

            if (!result.Succeeded)
            {
                var code = result?.Errors?.FirstOrDefault()?.Code;
                throw new CustomException(Lang.Find($"error_{code}"));
            }

            return result.Succeeded;
        }

        public async Task<IdentityResult> UpdateAsync(User identityUser)
        {
            return await _userManager.UpdateAsync(identityUser);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public SignInViewModel GenerateToken(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                //new Claim(ClaimTypes.Email, identityUser.Email),
                new Claim(ClaimTypes.Role, "Owner")
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

        public void Dispose()
        {
            _userManager?.Dispose();
        }
    }
}
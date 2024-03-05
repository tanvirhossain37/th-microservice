using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public async Task<bool> SaveAsync(User entity, string password)
        {
            var result = await _userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
            {
                var code = result?.Errors?.FirstOrDefault()?.Code;
                throw new CustomException(Lang.Find($"error_{code}"));
            }

            return result.Succeeded;
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public Task<bool> CheckPasswordAsync(User user, string password)
        {
            return _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GenerateToken(SignUpInputModel entity)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, entity.UserName),
                new Claim(ClaimTypes.Email, entity.Email),
                new Claim(ClaimTypes.Role, "Owner")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public void Dispose()
        {
            _userManager?.Dispose();
        }
    }
}
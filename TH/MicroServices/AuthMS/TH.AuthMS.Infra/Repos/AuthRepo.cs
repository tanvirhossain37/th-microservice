using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AuthMS.App;

namespace TH.AuthMS.Infra
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<User> _userManager;

        public AuthRepo(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> SaveAsync(User entity, string password)
        {
            var result = await _userManager.CreateAsync(entity, password);
            return result.Succeeded;
        }

        public void Dispose()
        {
            _userManager?.Dispose();
        }
    }
}
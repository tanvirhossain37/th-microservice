using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TH.AuthMS.App;
using TH.Common.Lang;
using TH.Common.Model;

namespace TH.AuthMS.API
{
    
    //public class CookieBasedAuthController : ControllerBase
    public class CookieBasedAuthController : CustomBaseController
    {
        public CookieBasedAuthController(HttpContextAccessor httpContextAccessor):base(httpContextAccessor)
        {
            
        }

        [HttpPost("GetAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var identityName = User.Identity.Name;
            return CustomResult(Lang.Find("success"), identityName);
        }

        [HttpPost("SignUpAsync")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpInputModel model)
        {
            if (model.UserName.Equals("string") && model.Password.Equals("string"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,"Tanvir"),
                    new Claim(ClaimTypes.Role, "AdminRole"),
                    new Claim("MyClaim 1", "Claim Value 1"),
                    new Claim("MyClaim 2", "Claim Value 2")
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return CustomResult(Lang.Find("success"), User);
            }

            return BadRequest();
        }

        public override void Dispose()
        {
            ;
        }
    }
}

using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TH.AuthMS.App;
using TH.Common.Lang;

namespace TH.AuthMS.API
{
    [AllowAnonymous]
    //public class AuthController : ControllerBase, IDisposable
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("SignUpAsync")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpInputModel model)
        {
            var result = await _authService.SignUpAsync(model);
            if (!result) return CustomResult(Lang.Find("error_not_found"), result, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), result, HttpStatusCode.OK);
        }

        [HttpPost("SignInAsync")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignInAsync([FromBody] SignUpInputModel model)
        {
            var token = await _authService.SignInAsync(model);
            if (string.IsNullOrWhiteSpace(token))
                return CustomResult(Lang.Find("error_not_found"), string.Empty, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), token);
        }

        public override void Dispose()
        {
            _authService?.Dispose();
        }
    }
}
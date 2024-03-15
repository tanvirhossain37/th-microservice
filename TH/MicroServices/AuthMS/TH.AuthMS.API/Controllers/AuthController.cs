using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MassTransit;
using TH.AuthMS.App;
using TH.Common.Lang;

namespace TH.AuthMS.API
{
    [AllowAnonymous]
    //public class AuthController : ControllerBase, IDisposable
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthController(IAuthService authService, IPublishEndpoint publishEndpoint)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        [HttpPost("SignUpAsync")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpInputModel model)
        {
            var viewModel = await _authService.SignUpAsync(model);
            if (!viewModel) return CustomResult(Lang.Find("error_not_found"), viewModel, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), viewModel);
        }

        [HttpPost("SignInAsync")]
        [ProducesResponseType(typeof(SignInViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInInputModel model)
        {
            var viewModel = await _authService.SignInAsync(model);
            if (viewModel is null)
                return CustomResult(Lang.Find("error_not_found"), null, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), viewModel);
        }

        [HttpPost("RefreshTokenAsync")]
        [ProducesResponseType(typeof(SignInViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenInputModel model)
        {
            var result = await _authService.RefreshToken(model);
            if (result is null)
                return CustomResult(Lang.Find("error_not_found"), null, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), result);
        }

        public override void Dispose()
        {
            _authService?.Dispose();
        }
    }
}
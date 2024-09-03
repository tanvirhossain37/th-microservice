using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MassTransit;
using Microsoft.Extensions.Options;
using TH.AuthMS.App;
using TH.AuthMS.Core;
using TH.Common.Lang;
using TH.Common.Model;

namespace TH.AuthMS.API
{
    [AllowAnonymous]
    //public class AuthController : ControllerBase, IDisposable
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly JwtConfiguration _configuration;

        public AuthController(IAuthService authService, IPublishEndpoint publishEndpoint, IOptions<JwtConfiguration> options,
            HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _configuration = options.Value;
        }

        [HttpPost("SignUpAsync")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpInputModel model)
        {
            //override
            model.IsAutoUserName = true;

            var viewModel = await _authService.SignUpAsync(model, DataFilter);
            if (viewModel is null) return CustomResult(Lang.Find("error_not_found"), viewModel, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), true);
        }

        [HttpPost("SignInAsync")]
        [ProducesResponseType(typeof(SignInViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignInAsync([FromBody] SignInInputModel model)
        {
            var viewModel = await _authService.SignInAsync(model, DataFilter);
            if (viewModel is null)
                return CustomResult(Lang.Find("error_not_found"), null, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), viewModel);
        }

        [HttpPost("RefreshTokenAsync")]
        [ProducesResponseType(typeof(SignInViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenInputModel model)
        {
            var result = await _authService.RefreshToken(model, DataFilter);
            if (result is null)
                return CustomResult(Lang.Find("error_not_found"), null, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), result);
        }

        [HttpPost("ActivateAccountAsync")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActivateAccountAsync([FromBody] ActivationCodeInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var viewModel = await _authService.ActivateAccountAsync(model, DataFilter);
            if (!viewModel) return CustomResult(Lang.Find("error_not_found"), viewModel, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), viewModel);
        }

        [HttpPost("ResendActivationCodeAsync")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ResendActivationCodeAsync([FromBody] ResendActivationCodeInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var viewModel = await _authService.ResendActivationCodeAsync(model, DataFilter);
            if (!viewModel) return CustomResult(Lang.Find("error_not_found"), viewModel, HttpStatusCode.NotFound);

            return CustomResult(Lang.Find("success"), viewModel);
        }

        [HttpPost("ForgotPasswordAsync")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            await _authService.ForgotPasswordAsync(model, DataFilter);

            return CustomResult(Lang.Find("success"));
        }

        [HttpPost("ResetPasswordAsync")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ForgotPasswordInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            await _authService.ResetPasswordAsync(model, DataFilter);

            return CustomResult(Lang.Find("success"));
        }

        public override void Dispose()
        {
            _authService?.Dispose();
        }
    }
}
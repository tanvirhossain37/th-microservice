using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MassTransit;
using Microsoft.Extensions.Options;
using TH.AuthMS.App;
using TH.AuthMS.Core;
using TH.Common.Lang;
using TH.Common.Model;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TH.AuthMS.API
{
    [AllowAnonymous]
    //public class AuthController : ControllerBase, IDisposable
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly JwtConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IPublishEndpoint publishEndpoint, IOptions<JwtConfiguration> options,
            HttpContextAccessor httpContextAccessor, ILogger<AuthController> logger) : base(httpContextAccessor)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

            _logger.LogTrace("Some just logged in!");
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

        [HttpGet("SignInGoogleAsync")]
        public async Task SignInGoogleAsync()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            return CustomResult(Lang.Find("success"), claims);
        }
        public override void Dispose()
        {
            _authService?.Dispose();
        }
    }
}
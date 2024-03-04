using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TH.AuthMS.App;

namespace TH.AuthMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase, IDisposable
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpInputModel model)
        {
            try
            {
                var result = await _authService.SignUpAsync(model);
                if (!result) return NotFound();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        public void Dispose()
        {
            _authService?.Dispose();
        }
    }
}
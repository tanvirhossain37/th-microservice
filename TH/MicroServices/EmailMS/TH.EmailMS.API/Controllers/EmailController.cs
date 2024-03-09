using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TH.Common.Lang;
using TH.EmailMS.API;

namespace TH.EmailMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class EmailController : ControllerBase
    public class EmailController : BaseController, IDisposable
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("SendEmailAsync")]
        [ProducesResponseType(typeof(EmailInputModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendEmailAsync([FromBody] EmailInputModel model)
        {
            try
            {
                if (model == null) throw new ArgumentNullException(nameof(model));

                var viewModel = await _emailService.SendEmailAsync(model);
                if (!viewModel) return CustomResult("error_email_sending_fail", viewModel, HttpStatusCode.BadRequest);

                return CustomResult(Lang.Find("success"), viewModel);
            }
            catch (ValidationException e)
            {
                return CustomResult(Lang.Find(e.Message), "", HttpStatusCode.BadRequest);
            }
            catch (ArgumentNullException e)
            {
                return CustomResult(Lang.Find("error_invalid_input"), "", HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                return CustomResult(Lang.Find("error_general"), "", HttpStatusCode.InternalServerError);
            }
        }

        public void Dispose()
        {
            _emailService?.Dispose();
        }
    }
}
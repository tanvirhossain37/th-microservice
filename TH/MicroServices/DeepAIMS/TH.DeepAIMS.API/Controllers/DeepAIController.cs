using System.Net;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TH.DeepAIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeepAIController : BaseController
    {
        private readonly IDeepAIService _deepAiService;

        public DeepAIController(IDeepAIService deepAiService)
        {
            _deepAiService = deepAiService ?? throw new ArgumentNullException(nameof(deepAiService));
        }

        [HttpPost("GenerateImageAsync")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GenerateImageAsync([FromBody] ImageInputModel model)
        {
            var result = await _deepAiService.GenerateImageAsync(model);

            return CustomResult(result, "Okay");
        }
    }
}
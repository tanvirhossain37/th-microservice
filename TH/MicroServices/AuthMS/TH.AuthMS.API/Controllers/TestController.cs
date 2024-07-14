using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TH.AuthMS.App;
using TH.AuthMS.App.Attributes;
using TH.Common.Lang;
using TH.Common.Model;

namespace TH.AuthMS.API.Controllers
{
    //public class TestController : ControllerBase
    [Authorize(Policy= "ClaimBasedPolicy")]
    //[CustomAuthorize]
    public class TestController : CustomBaseController
    {
        public TestController(HttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            
        }

        [HttpPost("Save")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
         [Authorize(Policy = "WritePolicy")]
        public async Task<IActionResult> Save()
        {
            var userIdentity = User.Identity;
            return CustomResult("You saved me!", "");
        }

        [HttpGet("Get")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Authorize(Policy = "ReadPolicy")]
        public async Task<IActionResult> Get()
        {
            var userIdentity = User.Identity;
            return CustomResult("You got me!", "");
        }

        public override void Dispose()
        {
            ;
        }
    }
}
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TH.AuthMS.App;
using TH.Common.Lang;

namespace TH.AuthMS.API.Controllers
{
    //public class TestController : ControllerBase
    public class TestController : CustomBaseController
    {
        [HttpGet("GetAsync")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SignInAsync()
        {
            return CustomResult("You got me!", "");
        }

        public override void Dispose()
        {
            ;
        }
    }
}
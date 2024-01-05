using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net;
using TH.MongoRnDMS.App;
using TH.MongoRnDMS.Common;

namespace TH.MongoRnDMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GardenController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(GardenViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(GardenFilterModel filter)
        {
            //var result1 = Lang.GetString("title");
            //Lang.ChangeLanguage("bn-BD");
            //var result2 = Lang.GetString("title");

            var result1 = Lang.Find("title");
            Lang.SetCultureCode("bn-BD");
            var result2 = Lang.Find("title");

            throw new CustomException();

            return Ok();
        }
    }
}
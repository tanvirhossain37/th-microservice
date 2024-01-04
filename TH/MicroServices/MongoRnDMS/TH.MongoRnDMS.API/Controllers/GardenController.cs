using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TH.Common.Lang;
using TH.MongoRnDMS.App;

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
            try
            {
                var result1 = Lang.GetString("title");
                Lang.ChangeLanguage("bn-BD");
                var result2 = Lang.GetString("title");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}

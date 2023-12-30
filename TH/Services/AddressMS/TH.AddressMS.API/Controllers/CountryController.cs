using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //public class CountryController : ControllerBase
    public class CountryController : BaseController, IDisposable
    {
        private readonly ICountryService _service;

        public CountryController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Country>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var countries = await _service.GetAllAsync();
                if (countries.Any())
                    return CustomResult("Success", countries);

                return CustomResult("Not found", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public void Dispose()
        {
            _service?.Dispose();
        }
    }
}

using CoreApiResponse;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TH.AddressMS.App;
using TH.AddressMS.Core;

namespace TH.AddressMS.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //public class AddressController : ControllerBase
    public class AddressController : BaseController, IDisposable
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SaveAsync(Address entity)
        {
            try
            {
                if (entity is null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                var result = await _service.SaveAsync(entity);
                if (result is not null)
                    return CustomResult("Saved");

                return CustomResult("Not saved", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Address>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllByClientIdAsync(string clientId)
        {
            try
            {
                if (clientId is null)
                {
                    throw new ArgumentNullException(nameof(clientId));
                }

                var addresses = await _service.GetAllByClientIdAsync(clientId);
                if (addresses.Any())
                    return CustomResult("Success", addresses);

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
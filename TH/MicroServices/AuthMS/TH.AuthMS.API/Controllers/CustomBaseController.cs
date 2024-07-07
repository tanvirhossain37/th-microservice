using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TH.AddressMS.App;

namespace TH.AuthMS.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //public class CustomBaseController : ControllerBase
    public abstract class CustomBaseController : BaseController, IDisposable
    {
        public DataFilter DataFilter { get; set; }
        public string BaseUrl { get; set; }

        public CustomBaseController()
        {
            DataFilter = new DataFilter
            {
                IncludeInactive = false
            };
            BaseUrl = "http://localhost:5000";
        }

        public abstract void Dispose();
    }
}
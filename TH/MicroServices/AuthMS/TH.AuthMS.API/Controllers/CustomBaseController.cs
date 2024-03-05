using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TH.AuthMS.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //public class CustomBaseController : ControllerBase
    public abstract class CustomBaseController : BaseController, IDisposable
    {
        public abstract void Dispose();
    }
}
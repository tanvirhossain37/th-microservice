using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace TH.Common.Model
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //public class CustomBaseController : ControllerBase
    public abstract class CustomBaseController : BaseController, IDisposable
    {
        public DataFilter DataFilter { get; set; }
        public string BaseUrl { get; set; }
        public UserResolver UserResolver { get; set; }

        public CustomBaseController(HttpContextAccessor httpContextAccessor)
        {

            DataFilter = new DataFilter
            {
                IncludeInactive = false
            };
            BaseUrl = "http://localhost:5000";

            var userResolver = new UserResolver();

            BaseUrl = httpContextAccessor.HttpContext.Request.Host.ToString();

            userResolver.UserName = httpContextAccessor.HttpContext.User.Identity.Name;
            userResolver.SpaceId = AuthorizeHelper.GetClaimValueByName("SpaceId", httpContextAccessor.HttpContext.User.Claims);

            //if (url.Contains("localhost"))
            //{

            //}
            //else
            //{
            //    sUserName = User.Identity.Name;
            //}

            UserResolver = userResolver;
        }

        public abstract void Dispose();
    }
}
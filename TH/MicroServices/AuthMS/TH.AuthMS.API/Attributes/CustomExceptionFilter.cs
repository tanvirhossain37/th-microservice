using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using TH.AuthMS.App;
using TH.Common.Lang;

namespace TH.AuthMS.API
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            try
            {
                if (context is null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                var exception = context.Exception;

                switch (exception)
                {
                    case CustomException:
                        context.Result = new ObjectResult(new { Message = exception.Message })
                        { StatusCode = (int?)HttpStatusCode.NotAcceptable };
                        break;
                    default:
                        context.Result = new ObjectResult(new { Message = Lang.Find("error_general") })
                        { StatusCode = (int?)HttpStatusCode.NotAcceptable };
                        break;
                }
            }
            catch (Exception e)
            {
                //log
                //_logger.LogInformation(e.Message);
                context.Result = new ObjectResult(new { Message = Lang.Find("error_general") })
                { StatusCode = (int?)HttpStatusCode.BadRequest };
            }
        }
    }
}
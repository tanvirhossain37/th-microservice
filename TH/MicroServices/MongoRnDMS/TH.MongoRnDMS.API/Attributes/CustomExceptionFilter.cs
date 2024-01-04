using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace TH.MongoRnDMS.API
{
    public class CustomExceptionFilter: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            try
            {
                if (context is null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                var exception = context?.Exception;

                //switch (exception)
                //{
                //    case CustomValidationException:
                //        context.Result = new ObjectResult(new { Message = _lang.Find("error_validation") })
                //        { StatusCode = (int?)HttpStatusCode.NotAcceptable };
                //        break;
                //    case CustomException:
                //        context.Result = new ObjectResult(new { Message = _lang.Find("error") })
                //        { StatusCode = (int?)HttpStatusCode.NotAcceptable };
                //        break;
                //    default:
                //        context.Result = new ObjectResult(new { Message = _lang.Find("error_general") })
                //        { StatusCode = (int?)HttpStatusCode.NotAcceptable };
                //        break;
                //}
            }
            catch (Exception)
            {
                //context.Result = new ObjectResult(new { Message = _lang.Find("error_general") })
                //{ StatusCode = (int?)HttpStatusCode.BadRequest };
            }
        }
    }
}

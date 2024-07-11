using System.Data;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TH.Common.Lang;

namespace TH.Common.Model;

public class CommonCustomExceptionFilter : ExceptionFilterAttribute
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
                    context.Result =
                        new ObjectResult(new
                            {
                                Message = exception.Message,
                                Data = "",
                                StatusCode = HttpStatusCode.NotAcceptable
                            })
                            { StatusCode = (int?)HttpStatusCode.NotAcceptable };
                    break;
                case NoNullAllowedException:
                    context.Result =
                        new ObjectResult(new
                            {
                                Message = exception.Message,
                                Data = "",
                                StatusCode = HttpStatusCode.NotAcceptable
                            })
                            { StatusCode = (int?)HttpStatusCode.NotAcceptable };
                    break;
                case UnauthorizedAccessException:
                    context.Result =
                        new ObjectResult(new
                            {
                                Message = exception.Message,
                                Data = "",
                                StatusCode = HttpStatusCode.Unauthorized
                            })
                            { StatusCode = (int?)HttpStatusCode.Unauthorized };
                    break;
                default:
                    context.Result =
                        new ObjectResult(new
                            {
                                Message = Lang.Lang.Find("error_general"),
                                Data = "",
                                StatusCode = HttpStatusCode.BadRequest
                            })
                            { StatusCode = (int?)HttpStatusCode.BadRequest };
                    break;
            }
        }
        catch (Exception e)
        {
            //log
            //_logger.LogInformation(e.Message);
            context.Result =
                new ObjectResult(new
                        { Message = Lang.Lang.Find("error_general"), Data = "", StatusCode = HttpStatusCode.InternalServerError })
                    { StatusCode = (int?)HttpStatusCode.InternalServerError };
        }
    }
}
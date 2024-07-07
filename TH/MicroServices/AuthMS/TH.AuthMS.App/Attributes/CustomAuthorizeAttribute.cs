using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace TH.AuthMS.App.Attributes;

public class CustomAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var controllerName = context.HttpContext.GetRouteData().Values["controller"].ToString();
        var actionName = context.HttpContext.GetRouteData().Values["action"].ToString();
        var requiredPermission = AuthorizeHelper.GetActionPermission(actionName);

        var claims = context.HttpContext.User.Claims;

        var permissions = AuthorizeHelper.GetPermissionsFromClaim(controllerName, claims);
        if (permissions is not null &&
            requiredPermission != 0 &&
            permissions.Contains(requiredPermission))
        {
            return;
        }

        context.Result = new UnauthorizedObjectResult("You don't have permissions!");

    }
}
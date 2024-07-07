using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TH.AuthMS.App;

public class ConventionBasedRequirement: IAuthorizationRequirement
{
}

public class ConventionBasedRequirementHandler : AuthorizationHandler<ConventionBasedRequirement>
{
    private HttpContextAccessor _httpContextAccessor;

    public ConventionBasedRequirementHandler(HttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ConventionBasedRequirement requirement)
    {
        var controllerName = _httpContextAccessor.HttpContext.GetRouteData().Values["controller"].ToString();
        var actionName = _httpContextAccessor.HttpContext.GetRouteData().Values["action"].ToString();
        var requiredPermission = AuthorizeHelper.GetActionPermission(actionName);

        var user = context.User;
        var claims = context.User.Claims;

        var userPermissions = AuthorizeHelper.GetPermissionsFromClaim(controllerName, claims);
        
        if (userPermissions is not null &&
            requiredPermission != 0 &&
            userPermissions.Contains(requiredPermission))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
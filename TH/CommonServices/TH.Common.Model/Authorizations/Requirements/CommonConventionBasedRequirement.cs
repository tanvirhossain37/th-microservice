using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace TH.Common.App;

public class CommonConventionBasedRequirement: IAuthorizationRequirement
{
}

public class CommonConventionBasedRequirementHandler : AuthorizationHandler<CommonConventionBasedRequirement>
{
    private HttpContextAccessor _httpContextAccessor;

    public CommonConventionBasedRequirementHandler(HttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommonConventionBasedRequirement requirement)
    {
        var controllerName = _httpContextAccessor.HttpContext.GetRouteData().Values["controller"].ToString();
        var actionName = _httpContextAccessor.HttpContext.GetRouteData().Values["action"].ToString();
        var requiredPermission = AuthorizeHelper.GetActionPermission(actionName);

        var user = context.User;
        
        var claims = context.User.Claims;

        var userPermissions = AuthorizeHelper.GetPermissionsFromClaim(controllerName, claims);

        if (userPermissions is not null &&
            !string.IsNullOrWhiteSpace(requiredPermission) &&
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
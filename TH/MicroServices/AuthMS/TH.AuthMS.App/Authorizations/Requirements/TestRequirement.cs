using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TH.AuthMS.App;

namespace TH.AuthMS.App
{
    public class TestRequirement : IAuthorizationRequirement
    {
    }

    public class TestRequirementHandler : AuthorizationHandler<TestRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestRequirement requirement)
        {
            var claims = context.User.Claims;

            var testPermissions = AuthorizeHelper.GetPermissionsFromClaim("Test", claims);

            if (testPermissions is not null &&
                testPermissions.Contains(1) &&
                testPermissions.Contains(2) &&
                testPermissions.Contains(3) &&
                testPermissions.Contains(4))
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
}
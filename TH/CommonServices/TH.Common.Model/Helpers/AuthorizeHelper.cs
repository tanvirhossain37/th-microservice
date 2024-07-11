using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TH.Common.App
{
    public static class AuthorizeHelper
    {
        public static IEnumerable<string> GetPermissionsFromClaim(string controllerName, IEnumerable<Claim> claims)
        {
            if (!claims.Any(t => t.Type.Equals(controllerName)))
            {
                return null;
            }

            return claims.Where(t => t.Type.Equals(controllerName)).Select(t => t.Value.To<string>());
        }

        public static string GetActionPermission(string actionName)
        {
            if (actionName.StartsWith("Save"))
            {
                return "Write";
            }
            else if (actionName.StartsWith("Update"))
            {
                return "Update";
            }
            else if (actionName.StartsWith("SoftDelete"))
            {
                return "SoftDelete";
            }
            else if (actionName.StartsWith("Delete"))
            {
                return "Delete";
            }
            else if (actionName.StartsWith("Find"))
            {
                return "Read";
            }
            else if (actionName.StartsWith("Get"))
            {
                return "Read";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
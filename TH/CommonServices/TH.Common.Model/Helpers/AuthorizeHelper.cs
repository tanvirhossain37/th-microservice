using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TH.Common.Model
{
    public static class AuthorizeHelper
    {
        public static IEnumerable<string> GetPermissionsFromClaim(string controllerName, IEnumerable<Claim> claims)
        {
            if (claims is not null)
            {
                if (!claims.Any(t => t.Type.Equals(controllerName)))
                {
                    return null;
                }

                return claims.Where(t => t.Type.Equals(controllerName)).Select(t => t.Value.To<string>());
            }

            return null;
        }

        public static string GetClaimValueByName(string name, IEnumerable<Claim> claims)
        {
            if (claims is not null)
            {
                return claims?.FirstOrDefault(x => x.Type.Equals(name, StringComparison.OrdinalIgnoreCase))?.Value;
            }

            return string.Empty;
        }

        public static string GetActionPermission(string actionName)
        {
            if (actionName.StartsWith("Save"))
            {
                return TS.Permissions.Write;
            }
            else if (actionName.StartsWith("Update"))
            {
                return TS.Permissions.Update;
            }
            else if (actionName.StartsWith("SoftDelete"))
            {
                return TS.Permissions.Archive;
            }
            else if (actionName.StartsWith("Delete"))
            {
                return TS.Permissions.Delete;
            }
            else if (actionName.StartsWith("Find"))
            {
                return TS.Permissions.Read;
            }
            else if (actionName.StartsWith("Get"))
            {
                return TS.Permissions.Read;
            }
            else
            {
                return TS.Permissions.None;
            }
        }
    }
}
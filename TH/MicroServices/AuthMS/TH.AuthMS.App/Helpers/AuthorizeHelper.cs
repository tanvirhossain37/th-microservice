using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public static class AuthorizeHelper
    {
        public static IEnumerable<int> GetPermissionsFromClaim(string controllerName, IEnumerable<Claim> claims)
        {
            if (!claims.Any(t => t.Type.Equals(controllerName)))
            {
                return null;
            }

            return claims.Where(t => t.Type.Equals(controllerName)).Select(t => t.Value.To<int>());
        }

        public static int GetActionPermission(string actionName)
        {
            if (actionName.StartsWith("Get"))
            {
                return 1;
            }
            else if (actionName.StartsWith("Save"))
            {
                return 2;
            }
            else if (actionName.StartsWith("Update"))
            {
                return 3;
            }
            else if (actionName.StartsWith("Delete"))
            {
                return 4;
            }
            else
            {
                return 0;
            }


            //switch (actionName)
            //{
            //    case "Get" : return 1;
            //    case "Save": return 2;
            //    case "Update": return 3;
            //    case "Delete": return 4;
            //    default: return 0;
            //}
        }
    }
}
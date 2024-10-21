using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.Common.Model;
using TH.CompanyMS.Core;

namespace TH.CompanyMS.App
{
    public partial interface IUserService
    {
        Task<User> SaveAsync(User entity, bool invitation, DataFilter dataFilter, bool commit = true);
    }
}

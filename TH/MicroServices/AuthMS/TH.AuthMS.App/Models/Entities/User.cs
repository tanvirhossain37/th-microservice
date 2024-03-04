using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.AuthMS.Core;

namespace TH.AuthMS.App
{
    public class User : IdentityUser, IEntity
    {
        public int UserTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
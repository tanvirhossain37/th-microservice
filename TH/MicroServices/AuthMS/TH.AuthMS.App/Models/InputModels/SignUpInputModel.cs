using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public class SignUpInputModel
    {
        public string Name { get; set; }
        public string Provider { get; set; }
        public string? PhotoUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? ReferralId { get; set; }
        public string? CompanyName { get; set; }
        public bool IsAutoUserName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
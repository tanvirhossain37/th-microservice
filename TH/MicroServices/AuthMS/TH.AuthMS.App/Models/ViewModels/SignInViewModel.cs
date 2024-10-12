using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TH.CompanyMS.App;
using TH.CompanyMS.Core;

namespace TH.AuthMS.App
{
    public class SignInViewModel
    {
        public string SpaceId { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiryTime { get; set; }
        public string RefreshToken { get; set; }
        public bool EmailConfirmed { get; set; }
        public SpaceSubscriptionViewModel SpaceSubscription { get; set; }
    }
}
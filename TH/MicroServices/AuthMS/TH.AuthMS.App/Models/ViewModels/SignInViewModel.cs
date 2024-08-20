using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public class SignInViewModel
    {
        public string SpaceId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiryTime { get; set; }
        public string RefreshToken { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
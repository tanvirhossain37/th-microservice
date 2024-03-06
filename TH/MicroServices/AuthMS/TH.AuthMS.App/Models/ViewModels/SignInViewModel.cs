using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public class SignInViewModel
    {
        public string Token { get; set; }
        public DateTime TokenExpireAt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpireAt { get; set; }

    }
}
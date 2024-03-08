using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public class RefreshTokenInputModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH.AuthMS.App
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}